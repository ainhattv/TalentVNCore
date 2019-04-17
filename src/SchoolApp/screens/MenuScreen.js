import React, { Component } from 'react';
import { View, StyleSheet, FlatList, Image, TouchableHighlight, TouchableOpacity } from 'react-native';
import { Container, Header, Content, Card, CardItem, Thumbnail, Text, Button, Icon, Left, Body, Right } from 'native-base';

export default class MenuScreen extends Component {

  static navigationOptions = {
    headerTitle: "Bản tin",
    headerLeft: (
      <Left style={{ padding: 10 }}>
        <Thumbnail small source={{ uri: 'https://cdn1.iconfinder.com/data/icons/office-1/128/4-512.png' }} />
      </Left>
    ),

  };

  DATA = [
    {
      key: 'a', imageUri: 'http://icons.iconarchive.com/icons/custom-icon-design/flatastic-8/256/Home-icon.png'
    },
    {
      key: 'a1', imageUri: 'http://icons.iconarchive.com/icons/custom-icon-design/flatastic-8/256/Home-icon.png'
    },
    {
      key: 'a2', imageUri: 'http://icons.iconarchive.com/icons/custom-icon-design/flatastic-8/256/Home-icon.png'
    },
    {
      key: 'a3', imageUri: 'http://icons.iconarchive.com/icons/custom-icon-design/flatastic-8/256/Home-icon.png'
    },
    {
      key: 'a4', imageUri: 'http://icons.iconarchive.com/icons/custom-icon-design/flatastic-8/256/Home-icon.png'
    },
    {
      key: 'a5', imageUri: 'http://icons.iconarchive.com/icons/custom-icon-design/flatastic-8/256/Home-icon.png'
    },
    {
      key: 'a6', imageUri: 'http://icons.iconarchive.com/icons/custom-icon-design/flatastic-8/256/Home-icon.png'
    },
    {
      key: 'a7', imageUri: 'http://icons.iconarchive.com/icons/custom-icon-design/flatastic-8/256/Home-icon.png'
    },
    {
      key: 'a8', imageUri: 'http://icons.iconarchive.com/icons/custom-icon-design/flatastic-8/256/Home-icon.png'
    }
  ];

  _onPress = () => {

    alert("This is Card Header")
  }

  renderItem({ item, index }) {
    return <View style={{
      flex: 1,
      margin: 10,
      padding: 10,
      minWidth: 80,
      maxWidth: 110,
      height: 80,
      maxHeight: 80,
      borderRadius: 4,
      borderWidth: 0.5,
      borderColor: '#d6d7da',
      justifyContent: 'center',
      backgroundColor: '#fffff',
    }} >
      <TouchableOpacity onPress={() => alert("This is Card Header")}>
        <Thumbnail large source={{ uri: item.imageUri }} />
      </TouchableOpacity>
    </View>
  }
  render() {
    return (<FlatList
      contentContainerStyle={styles.list}
      data={this.DATA}
      renderItem={this.renderItem}
      numColumns={3}
    />);
  }
}

const styles = StyleSheet.create({
  list: {
    justifyContent: 'center',
    flexDirection: 'column',
  }
});
