import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import {
  createNativeStackNavigator
} from '@react-navigation/native-stack';

import ItemsScreen from '../screens/items/ItemsScreen';
import CreateItemScreen from '../screens/items/CreateItemScreen';
import EditItemScreen from '../screens/items/EditItemScreen';
import { RootStackParamList } from './types';

const Stack = createNativeStackNavigator<RootStackParamList>();

function RootNavigator(): React.JSX.Element {
  return (
    <NavigationContainer>
      <Stack.Navigator
        screenOptions={{
          headerStyle: { backgroundColor: '#007AFF' },
          headerTintColor: '#fff',
          headerTitleStyle: { fontWeight: 'bold' },
        }}
      >
        <Stack.Screen
          name="Items"
          component={ItemsScreen}
          options={{ title: 'Items' }}
        />
        <Stack.Screen
          name="CreateItem"
          component={CreateItemScreen}
          options={{ title: 'Create Item' }}
        />
        <Stack.Screen
          name="EditItem"
          component={EditItemScreen}
          options={{ title: 'Edit Item' }}
        />
      </Stack.Navigator>
    </NavigationContainer>
  );
}

export default RootNavigator;