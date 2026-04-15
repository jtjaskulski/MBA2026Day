export interface UnitOfMeasurement {
  idUnitOfMeasurement: number;
  name: string | null;
  description: string | null;
  isActive: boolean;
}

// Kategoria
export interface Category {
  idCategory: number;
  name: string | null;
  description: string | null;
  isActive: boolean;
}

// Klient
export interface Client {
  idClient: number;
  name: string | null;
  address: string | null;
  phoneNumber: string | null;
  isActive: boolean;
}

// Pracownik
export interface Worker {
  idWorker: number;
  firstName: string | null;
  lastName: string | null;
  isActive: boolean;
  login: string;
  password?: string;
}

// Produkt (ItemDto z backendu)
export interface Item {
  idItem: number;
  name: string | null;
  description: string | null;
  idCategory: number;
  categoryName: string | null;
  price: number | null;
  quantity: number | null;
  idUnitOfMeasurement: number | null;
  unitName: string | null;
  code: string | null;
  isActive: boolean;
}

// Request types (dla Create/Update)
export interface CreateItemRequest {
  name: string;
  description?: string;
  idCategory: number;
  price?: number;
  quantity?: number;
  fotoUrl?: string;
  idUnitOfMeasurement?: number;
  code?: string;
}

export interface UpdateItemRequest extends CreateItemRequest {
  idItem: number;
  isActive: boolean;
}

// Client requests
export interface CreateClientRequest {
  name: string;
  address?: string;
  phoneNumber?: string;
}

export interface UpdateClientRequest extends CreateClientRequest {
  idClient: number;
  isActive: boolean;
}

// Worker requests
export interface CreateWorkerRequest {
  firstName: string;
  lastName: string;
  login: string;
}

export interface UpdateWorkerRequest extends CreateWorkerRequest {
  idWorker: number;
  isActive: boolean;
}

// Order
export interface OrderItem {
  idOrderItem: number;
  idItem: number;
  itemName: string | null;
  quantity: number | null;
  price: number | null;
  isActive: boolean;
}

export interface Order {
  idOrder: number;
  dataOrder: string | null;
  idClient: number | null;
  clientName: string | null;
  idWorker: number | null;
  workerName: string | null;
  notes: string | null;
  deliveryDate: string | null;
  orderItems: OrderItem[];
}

export interface CreateOrderItemRequest {
  idItem: number;
  quantity: number;
}

export interface CreateOrderRequest {
  idClient?: number;
  idWorker?: number;
  notes?: string;
  deliveryDate?: string;
  orderItems: CreateOrderItemRequest[];
}

export interface UpdateOrderRequest extends CreateOrderRequest {
  idOrder: number;
}

// Cart (local only)
export interface CartItem {
  item: Item;
  quantity: number;
}