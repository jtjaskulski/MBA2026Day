import type { Item } from '../types/models';

export type RootStackParamList = {
  Home: undefined;
  Details: { 
    itemId: number; 
    itemName: string;
  };
  Profile: { 
    userId: number;
  };
  Items: undefined;
  CreateItem: undefined;
  EditItem: { item: Item };
  Categories: undefined;
  Units: undefined;
  Clients: undefined;
  Workers: undefined;
  Orders: undefined;
  Cart: undefined;
  CreateOrder: undefined;
  EditOrder: { orderId: number };
};