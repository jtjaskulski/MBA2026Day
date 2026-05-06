import {
  View,
  Text,
  FlatList,
  TouchableOpacity,
  StyleSheet,
  Alert,
} from 'react-native';
import { useCart } from '../../context/CartContext';
import type { CartItem } from '../../types/models';
import { NativeStackScreenProps } from '@react-navigation/native-stack';
import { RootStackParamList } from '../../navigation/types';

type Props = NativeStackScreenProps<RootStackParamList, 'Cart'>;

function CartScreen({ navigation }: Props) {
  const { cartItems, removeFromCart, updateQuantity, clearCart, getCartTotal } =
    useCart();

  const handleClearCart = () => {
    Alert.alert('Clear Cart', 'Remove all items from cart?', [
      { text: 'Cancel', style: 'cancel' },
      { text: 'Clear', style: 'destructive', onPress: clearCart },
    ]);
  };

  const renderItem = ({ item: cartItem }: { item: CartItem }) => (
    <View style={styles.itemCard}>
      <View style={styles.itemContent}>
        <Text style={styles.itemName}>{cartItem.item.name || 'N/A'}</Text>
        <Text style={styles.itemPrice}>
          {cartItem.item.price?.toFixed(2) || '0.00'} zl x {cartItem.quantity} ={' '}
          {((cartItem.item.price ?? 0) * cartItem.quantity).toFixed(2)} zl
        </Text>
      </View>

      <View style={styles.quantityRow}>
        <TouchableOpacity
          style={styles.qtyButton}
          onPress={() =>
            updateQuantity(cartItem.item.idItem, cartItem.quantity - 1)
          }
        >
          <Text style={styles.qtyButtonText}>-</Text>
        </TouchableOpacity>
        <Text style={styles.qtyText}>{cartItem.quantity}</Text>
        <TouchableOpacity
          style={styles.qtyButton}
          onPress={() =>
            updateQuantity(cartItem.item.idItem, cartItem.quantity + 1)
          }
        >
          <Text style={styles.qtyButtonText}>+</Text>
        </TouchableOpacity>
        <TouchableOpacity
          style={styles.removeButton}
          onPress={() => removeFromCart(cartItem.item.idItem)}
        >
          <Text style={styles.removeButtonText}>X</Text>
        </TouchableOpacity>
      </View>
    </View>
  );

  return (
    <View style={styles.container}>
      <View style={styles.header}>
        <Text style={styles.title}>Cart ({cartItems.length})</Text>
        {cartItems.length > 0 && (
          <TouchableOpacity style={styles.clearButton} onPress={handleClearCart}>
            <Text style={styles.clearButtonText}>Clear</Text>
          </TouchableOpacity>
        )}
      </View>

      <FlatList
        data={cartItems}
        renderItem={renderItem}
        keyExtractor={ci => ci.item.idItem.toString()}
        contentContainerStyle={styles.listContent}
        ListEmptyComponent={
          <Text style={styles.emptyText}>Cart is empty. Add items first.</Text>
        }
      />

      {cartItems.length > 0 && (
        <View style={styles.footer}>
          <Text style={styles.totalText}>
            Total: {getCartTotal().toFixed(2)} zl
          </Text>
          <TouchableOpacity
            style={styles.orderButton}
            onPress={() => navigation.navigate('CreateOrder')}
          >
            <Text style={styles.orderButtonText}>Create Order</Text>
          </TouchableOpacity>
        </View>
      )}
    </View>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1, backgroundColor: '#f5f5f5' },
  header: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    padding: 16,
    backgroundColor: '#fff',
    borderBottomWidth: 1,
    borderBottomColor: '#ddd',
  },
  title: { fontSize: 24, fontWeight: 'bold', color: '#333' },
  clearButton: {
    backgroundColor: '#F44336',
    paddingHorizontal: 16,
    paddingVertical: 8,
    borderRadius: 8,
  },
  clearButtonText: { color: '#fff', fontWeight: '600' },
  listContent: { padding: 16 },
  itemCard: {
    backgroundColor: '#fff',
    padding: 12,
    marginBottom: 12,
    borderRadius: 8,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.1,
    shadowRadius: 2,
    elevation: 2,
  },
  itemContent: { marginBottom: 8 },
  itemName: { fontSize: 16, fontWeight: '600', color: '#333' },
  itemPrice: { fontSize: 14, color: '#007AFF', marginTop: 4, fontWeight: '500' },
  quantityRow: { flexDirection: 'row', alignItems: 'center', gap: 10 },
  qtyButton: {
    backgroundColor: '#007AFF',
    width: 36,
    height: 36,
    borderRadius: 18,
    justifyContent: 'center',
    alignItems: 'center',
  },
  qtyButtonText: { color: '#fff', fontSize: 18, fontWeight: 'bold' },
  qtyText: { fontSize: 18, fontWeight: '600', minWidth: 30, textAlign: 'center' },
  removeButton: {
    backgroundColor: '#F44336',
    width: 36,
    height: 36,
    borderRadius: 18,
    justifyContent: 'center',
    alignItems: 'center',
    marginLeft: 'auto',
  },
  removeButtonText: { color: '#fff', fontSize: 16, fontWeight: 'bold' },
  footer: {
    padding: 16,
    backgroundColor: '#fff',
    borderTopWidth: 1,
    borderTopColor: '#ddd',
  },
  totalText: {
    fontSize: 20,
    fontWeight: 'bold',
    color: '#333',
    marginBottom: 12,
    textAlign: 'center',
  },
  orderButton: {
    backgroundColor: '#4CAF50',
    paddingVertical: 14,
    borderRadius: 8,
    alignItems: 'center',
  },
  orderButtonText: { color: '#fff', fontSize: 18, fontWeight: '700' },
  emptyText: {
    textAlign: 'center',
    fontSize: 16,
    color: '#999',
    marginTop: 40,
  },
});

export default CartScreen;