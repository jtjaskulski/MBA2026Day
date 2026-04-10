/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 *
 * @format
 */

import { NewAppScreen } from '@react-native/new-app-screen';
import { StatusBar, ScrollView, StyleSheet, useColorScheme, View, Text } from 'react-native';
import {
  SafeAreaProvider,
  useSafeAreaInsets,
} from 'react-native-safe-area-context';
import RootNavigator from './src/navigation/RootNavigator';
import Greeting from './src/components/Greeting';

function App() {
  const isDarkMode = useColorScheme() === 'dark';

  return ( 
      <RootNavigator /> 
  );
}


export default App;
