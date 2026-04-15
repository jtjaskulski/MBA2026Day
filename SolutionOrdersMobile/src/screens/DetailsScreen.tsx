import React from 'react';
import { View, Text, Button, StyleSheet } from 'react-native';
import { NativeStackScreenProps } from '@react-navigation/native-stack';
import { RootStackParamList } from '../navigation/types';

type Props = NativeStackScreenProps<RootStackParamList, 'Details'>;

const DetailsScreen: React.FC<Props> = ({ route, navigation }) => {
  // Get parameters from route
  const { itemId, itemName } = route.params;

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Details of product</Text>
      
      <Text style={styles.info}>ID: {itemId}</Text>
      <Text style={styles.info}>Name: {itemName}</Text>

      <Button 
        title="Back to Home" 
        onPress={() => navigation.goBack()}
      />
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 20,
    backgroundColor: '#f5f5f5',
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom: 20,
    color: '#333',
  },
  info: {
    fontSize: 16,
    color: '#666',
    marginBottom: 10,
  },
});

export default DetailsScreen;