import { createContext, useContext, useState, useCallback, ReactNode } from 'react';
import type { Item, CartItem } from '../types/models';

interface CartContextType {
  cartItems: CartItem[];
  addToCart: (item: Item, quantity?: number) => void;
  removeFromCart: (idItem: number) => void;
  updateQuantity: (idItem: number, quantity: number) => void;
  clearCart: () => void;
  getCartTotal: () => number;
  getCartCount: () => number;
}

const CartContext = createContext<CartContextType | undefined>(undefined);

export function CartProvider({ children }: { children: ReactNode }) {
  const [cartItems, setCartItems] = useState<CartItem[]>([]);

  const addToCart = useCallback((item: Item, quantity: number = 1) => {
    setCartItems(prev => {
      const existing = prev.find(ci => ci.item.idItem === item.idItem);
      if (existing) {
        return prev.map(ci =>
          ci.item.idItem === item.idItem
            ? { ...ci, quantity: ci.quantity + quantity }
            : ci,
        );
      }
      return [...prev, { item, quantity }];
    });
  }, []);

  const removeFromCart = useCallback((idItem: number) => {
    setCartItems(prev => prev.filter(ci => ci.item.idItem !== idItem));
  }, []);

  const updateQuantity = useCallback((idItem: number, quantity: number) => {
    if (quantity <= 0) {
      setCartItems(prev => prev.filter(ci => ci.item.idItem !== idItem));
      return;
    }
    setCartItems(prev =>
      prev.map(ci =>
        ci.item.idItem === idItem ? { ...ci, quantity } : ci,
      ),
    );
  }, []);

  const clearCart = useCallback(() => {
    setCartItems([]);
  }, []);

  const getCartTotal = useCallback(() => {
    return cartItems.reduce(
      (sum, ci) => sum + (ci.item.price ?? 0) * ci.quantity,
      0,
    );
  }, [cartItems]);

  const getCartCount = useCallback(() => {
    return cartItems.reduce((sum, ci) => sum + ci.quantity, 0);
  }, [cartItems]);

  return (
    <CartContext.Provider
      value={{
        cartItems,
        addToCart,
        removeFromCart,
        updateQuantity,
        clearCart,
        getCartTotal,
        getCartCount,
      }}
    >
      {children}
    </CartContext.Provider>
  );
}

export function useCart() {
  const context = useContext(CartContext);
  if (!context) {
    throw new Error('useCart must be used within CartProvider');
  }
  return context;
}