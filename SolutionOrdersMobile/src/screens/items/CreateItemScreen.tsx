import {
  View,
  TextInput,
  Button,
  StyleSheet,
  Alert,
  ScrollView,
  Text,
  ActivityIndicator,
  TouchableOpacity,
} from 'react-native';
import { useState, useEffect } from 'react';
import { useItems } from '../../context/ItemsContext';
import apiService from '../../api/apiService';
import type { Category, UnitOfMeasurement } from '../../types/models';

import { NativeStackScreenProps } from '@react-navigation/native-stack';
import { RootStackParamList } from '../../navigation/types';

type Props = NativeStackScreenProps<RootStackParamList, 'CreateItem'>;

function CreateItemScreen({ navigation }: Props) {
  const { createItem } = useItems();

  // Form state
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [price, setPrice] = useState('');
  const [quantity, setQuantity] = useState('');
  const [code, setCode] = useState('');
  const [idCategory, setIdCategory] = useState('');
  const [idUnitOfMeasurement, setIdUnitOfMeasurement] = useState('');

  // Dropdowns state
  const [categories, setCategories] = useState<Category[]>([]);
  const [units, setUnits] = useState<UnitOfMeasurement[]>([]);
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);

  // Pobranie kategorii i jednostek
  useEffect(() => {
    const loadData = async () => {
      try {
        const [cats, unts] = await Promise.all([
          apiService.getCategories(),
          apiService.getUnitOfMeasurements(),
        ]);
        setCategories(cats);
        setUnits(unts);
      } catch (error) {
        Alert.alert('Error', 'Failed to load data: ' + (error as Error).message);
      } finally {
        setLoading(false);
      }
    };
    loadData();
  }, []);

  const handleSubmit = async () => {
    // Validation
    if (!name.trim()) {
      Alert.alert('Error', 'Please enter a product name');
      return;
    }
    if (!idCategory) {
      Alert.alert('Error', 'Please select a category');
      return;
    }

    try {
      setSubmitting(true);
      await createItem({
        name: name.trim(),
        description: description.trim() || undefined,
        price: price ? parseFloat(price) : undefined,
        quantity: quantity ? parseFloat(quantity) : undefined,
        idCategory: parseInt(idCategory),
        idUnitOfMeasurement: idUnitOfMeasurement
          ? parseInt(idUnitOfMeasurement)
          : undefined,
        code: code.trim() || undefined,
      });

      Alert.alert('Success', 'Product created successfully', [
        { text: 'OK', onPress: () => navigation.goBack() },
      ]);
    } catch (error) {
      Alert.alert('Error', (error as Error).message);
    } finally {
      setSubmitting(false);
    }
  };

  if (loading) {
    return (
      <View style={styles.centerContainer}>
        <ActivityIndicator size="large" />
      </View>
    );
  }

  return (
    <ScrollView style={styles.container}>
      <View style={styles.form}>
        <Text style={styles.label}>Product Name *</Text>
        <TextInput
          style={styles.input}
          placeholder="Enter name"
          value={name}
          onChangeText={setName}
          editable={!submitting}
        />

        <Text style={styles.label}>Description</Text>
        <TextInput
          style={[styles.input, styles.multiline]}
          placeholder="Enter description"
          value={description}
          onChangeText={setDescription}
          multiline
          numberOfLines={3}
          editable={!submitting}
        />

        <Text style={styles.label}>Price</Text>
        <TextInput
          style={styles.input}
          placeholder="Enter price"
          value={price}
          onChangeText={setPrice}
          keyboardType="decimal-pad"
          editable={!submitting}
        />

        <Text style={styles.label}>Quantity</Text>
        <TextInput
          style={styles.input}
          placeholder="Enter quantity"
          value={quantity}
          onChangeText={setQuantity}
          keyboardType="decimal-pad"
          editable={!submitting}
        />

        <Text style={styles.label}>Product Code</Text>
        <TextInput
          style={styles.input}
          placeholder="Enter code"
          value={code}
          onChangeText={setCode}
          editable={!submitting}
        />

        {/* Category - using TouchableOpacity instead of Button for better Android support */}
        <Text style={styles.label}>Category *</Text>
        <View style={styles.pickerContainer}>
          {categories.map(cat => {
            const isSelected = idCategory === cat.idCategory.toString();
            return (
              <TouchableOpacity
                key={cat.idCategory}
                style={[
                  styles.chip,
                  isSelected && styles.chipSelected,
                ]}
                onPress={() => setIdCategory(cat.idCategory.toString())}
                disabled={submitting}
                activeOpacity={0.7}
              >
                <Text style={[
                  styles.chipText,
                  isSelected && styles.chipTextSelected,
                ]}>
                  {cat.name || 'Empty'}
                </Text>
              </TouchableOpacity>
            );
          })}
        </View>

        {/* Unit of Measurement */}
        <Text style={styles.label}>Unit of Measurement</Text>
        <View style={styles.pickerContainer}>
          <TouchableOpacity
            style={[
              styles.chip,
              idUnitOfMeasurement === '' && styles.chipSelected,
            ]}
            onPress={() => setIdUnitOfMeasurement('')}
            disabled={submitting}
            activeOpacity={0.7}
          >
            <Text style={[
              styles.chipText,
              idUnitOfMeasurement === '' && styles.chipTextSelected,
            ]}>
              None
            </Text>
          </TouchableOpacity>
          {units.map(unit => {
            const isSelected = idUnitOfMeasurement === unit.idUnitOfMeasurement.toString();
            return (
              <TouchableOpacity
                key={unit.idUnitOfMeasurement}
                style={[
                  styles.chip,
                  isSelected && styles.chipSelected,
                ]}
                onPress={() =>
                  setIdUnitOfMeasurement(unit.idUnitOfMeasurement.toString())
                }
                disabled={submitting}
                activeOpacity={0.7}
              >
                <Text style={[
                  styles.chipText,
                  isSelected && styles.chipTextSelected,
                ]}>
                  {unit.name || 'None'}
                </Text>
              </TouchableOpacity>
            );
          })}
        </View>

        <View style={styles.buttons}>
          <Button
            title="Cancel"
            onPress={() => navigation.goBack()}
            color="#999"
            disabled={submitting}
          />
          <Button
            title={submitting ? 'Submitting...' : 'Create'}
            onPress={handleSubmit}
            disabled={submitting}
          />
        </View>
      </View>
    </ScrollView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#f5f5f5',
  },
  centerContainer: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
  form: {
    padding: 16,
  },
  label: {
    fontSize: 14,
    fontWeight: '600',
    color: '#333',
    marginBottom: 8,
    marginTop: 16,
  },
  input: {
    borderWidth: 1,
    borderColor: '#ddd',
    borderRadius: 8,
    padding: 12,
    fontSize: 16,
    backgroundColor: '#fff',
  },
  multiline: {
    height: 80,
    textAlignVertical: 'top',
  },
  pickerContainer: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 8,
    marginBottom: 8,
  },
  // Style dla chipów (kategoria/jednostka)
  chip: {
    paddingHorizontal: 16,
    paddingVertical: 10,
    borderRadius: 8,
    backgroundColor: '#e0e0e0',
    borderWidth: 2,
    borderColor: '#e0e0e0',
  },
  chipSelected: {
    backgroundColor: '#007AFF',
    borderColor: '#007AFF',
  },
  chipText: {
    fontSize: 14,
    fontWeight: '600',
    color: '#333',
  },
  chipTextSelected: {
    color: '#fff',
  },
  buttons: {
    flexDirection: 'row',
    columnGap: 10,
    marginTop: 20,
    marginBottom: 30,
  },
});

export default CreateItemScreen;