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
import Greeting from './src/components/Greeting';

function App() {
  const isDarkMode = useColorScheme() === 'dark';

  return (
    // <SafeAreaProvider>
    //   <StatusBar barStyle={isDarkMode ? 'light-content' : 'dark-content'} />
    //   <AppContent />
    // </SafeAreaProvider>
    //  <View style={styles.container}>
    //   <Text style={styles.title}>Hello React Native!</Text>
    //   <Text style={styles.subtitle}>with TypeScript 🚀</Text>
    // </View>
    <SafeAreaProvider>
      <StatusBar barStyle={isDarkMode ? 'light-content' : 'dark-content'} />
      <ScrollView style={styles.container}>
        <Greeting name="Emilia" isVip={true} />
        <Greeting name="Błażej Leśniak" isVip={true} />
        <Greeting name="Albi Lubila Mayamwene" age={22} isVip={true} />
        <Greeting name="Paweł Tokarczyk" isVip={true} />
        <Greeting name="Adrian Kurcharski" isVip={true} />
        <Greeting name="Klimkiewicz" isVip={true} />
        <Greeting name="Jakub Jaskulski" age={33} isVip={false} />
      </ScrollView>
    </SafeAreaProvider>
  );
}

function AppContent() {
  const safeAreaInsets = useSafeAreaInsets();

  return (
    <View style={styles.container}>
      <NewAppScreen
        templateFileName="App.tsx"
        safeAreaInsets={safeAreaInsets}
      />
    </View>
  );
}

// const styles = StyleSheet.create({
//   container: {
//     flex: 1,
//   },
// });

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#f5f5f5',
  },
});

// const styles = StyleSheet.create({
//   container: {
//     flex: 1,
//     justifyContent: 'center',
//     alignItems: 'center',
//     backgroundColor: '#f5f5f5',
//   },
//   title: {
//     fontSize: 24,
//     fontWeight: 'bold',
//     color: '#333',
//   },
//   subtitle: {
//     fontSize: 16,
//     color: '#666',
//     marginTop: 8,
//   },
// });

export default App;
