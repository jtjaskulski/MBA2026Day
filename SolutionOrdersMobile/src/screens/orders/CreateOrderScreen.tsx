import { useState, useEffect } from 'react';
import {
  View,
  Text,
  TextInput,
  TouchableOpacity,
  StyleSheet,
  ScrollView,
  Alert,
  ActivityIndicator,
} from 'react-native';
import { Picker } from '@react-native-picker/picker';
import { useCart } from '../../context/CartContext';
import apiService from '../../api/apiService';
import type { Client, Worker } from '../../types/models';
import { NativeStackScreenProps } from '@react-navigation/native-stack';
import { RootStackParamList } from '../../navigation/types';

type Props = NativeStackScreenProps<RootStackParamList, 'CreateOrder'>;

function CreateOrderScreen({ navigation }: Props) {
  const { cartItems, clearCart, getCartTotal } = useCart();

  const [clients, setClients] = useState<Client[]>([]);
  const [workers, setWorkers] = useState<Worker[]>([]);
  const [selectedClientId, setSelectedClientId] = useState<number | undefined>();
  const [selectedWorkerId, setSelectedWorkerId] = useState<number | undefined>();
  const [notes, setNotes] = useState('');
  const [deliveryDate, setDeliveryDate] = useState('');
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);

  useEffect(() => {
    const loadData = async () => {
      try {
        const [clientsData, workersData] = await Promise.all([
          apiService.getClients(),
          apiService.getWorkers(),
        ]);
        setClients(clientsData);
        setWorkers(workersData);
        if (clientsData.length > 0) {
          setSelectedClientId(clientsData[0].idClient);
        }
        if (workersData.length > 0) {
          setSelectedWorkerId(workersData[0].idWorker);
        }
      } catch (err) {
        Alert.alert('Error', 'Failed to load clients/workers');
      } finally {
        setLoading(false);
      }
    };
    loadData();
  }, []);

  const handleSubmit = async () => {
    if (cartItems.length === 0) {
      Alert.alert('Error', 'Cart is empty');
      return;
    }

    setSubmitting(true);
    try {
      await apiService.createOrder({
        idClient: selectedClientId,
        idWorker: selectedWorkerId,
        notes: notes || undefined,
        deliveryDate: deliveryDate || undefined,
        orderItems: cartItems.map(ci => ({
          idItem: ci.item.idItem,
          quantity: ci.quantity,
        })),
      });

      clearCart();
      Alert.alert('Success', 'Order created successfully!', [
        { text: 'OK', onPress: () => navigation.popToTop() },
      ]);
    } catch (err) {
      Alert.alert('Error', (err as Error).message);
    } finally {
      setSubmitting(false);
    }
  };

  if (loading) {
    return (
      <View style={styles.centerContainer}>
        <ActivityIndicator size="large" color="#007AFF" />
      </View>
    );
  }

  return (
    <ScrollView style={styles.container}>
      <Text style={styles.sectionTitle}>Client</Text>
      <View style={styles.pickerContainer}>
        <Picker
          selectedValue={selectedClientId}
          onValueChange={val => setSelectedClientId(val)}
        >
          {clients.map(c => (
            <Picker.Item key={c.idClient} label={c.name || 'N/A'} value={c.idClient} />
          ))}
        </Picker>
      </View>

      <Text style={styles.sectionTitle}>Worker</Text>
      <View style={styles.pickerContainer}>
        <Picker
          selectedValue={selectedWorkerId}
          onValueChange={val => setSelectedWorkerId(val)}
        >
          {workers.map(w => (
            <Picker.Item
              key={w.idWorker}
              label={`${w.firstName || ''} ${w.lastName || ''}`}
              value={w.idWorker}
            />
          ))}
        </Picker>
      </View>

      <Text style={styles.sectionTitle}>Notes</Text>
      <TextInput
        style={styles.input}
        value={notes}
        onChangeText={setNotes}
        placeholder="Optional notes..."
        multiline
      />

      <Text style={styles.sectionTitle}>Delivery Date (YYYY-MM-DD)</Text>
      <TextInput
        style={styles.input}
        value={deliveryDate}
        onChangeText={setDeliveryDate}
        placeholder="e.g. 2026-04-20"
      />

      <Text style={styles.sectionTitle}>
        Order Items ({cartItems.length})
      </Text>
      {cartItems.map(ci => (
        <View key={ci.item.idItem} style={styles.summaryItem}>
          <Text style={styles.summaryName}>{ci.item.name}</Text>
          <Text style={styles.summaryDetail}>
            {ci.quantity} x {ci.item.price?.toFixed(2) || '0.00'} zl
          </Text>
        </View>
      ))}

      <View style={styles.totalRow}>
        <Text style={styles.totalLabel}>Total:</Text>
        <Text style={styles.totalValue}>{getCartTotal().toFixed(2)} zl</Text>
      </View>

      <TouchableOpacity
        style={[styles.submitButton, submitting && styles.submitButtonDisabled]}
        onPress={handleSubmit}
        disabled={submitting}
      >
        <Text style={styles.submitButtonText}>
          {submitting ? 'Submitting...' : 'Submit Order'}
        </Text>
      </TouchableOpacity>

      <View style={styles.bottomSpacer} />
    </ScrollView>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1, backgroundColor: '#f5f5f5', padding: 16 },
  centerContainer: { flex: 1, justifyContent: 'center', alignItems: 'center' },
  sectionTitle: {
    fontSize: 16,
    fontWeight: '700',
    color: '#333',
    marginTop: 16,
    marginBottom: 8,
  },
  pickerContainer: {
    backgroundColor: '#fff',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#ddd',
    overflow: 'hidden',
  },
  input: {
    backgroundColor: '#fff',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#ddd',
    padding: 12,
    fontSize: 16,
    minHeight: 44,
  },
  summaryItem: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    backgroundColor: '#fff',
    padding: 12,
    marginBottom: 4,
    borderRadius: 6,
  },
  summaryName: { fontSize: 14, fontWeight: '600', color: '#333' },
  summaryDetail: { fontSize: 14, color: '#007AFF' },
  totalRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    paddingVertical: 16,
    borderTopWidth: 1,
    borderTopColor: '#ddd',
    marginTop: 8,
  },
  totalLabel: { fontSize: 20, fontWeight: 'bold', color: '#333' },
  totalValue: { fontSize: 20, fontWeight: 'bold', color: '#007AFF' },
  submitButton: {
    backgroundColor: '#4CAF50',
    paddingVertical: 16,
    borderRadius: 8,
    alignItems: 'center',
    marginTop: 8,
  },
  submitButtonDisabled: { opacity: 0.6 },
  submitButtonText: { color: '#fff', fontSize: 18, fontWeight: '700' },
  bottomSpacer: { height: 40 },
});

export default CreateOrderScreen;