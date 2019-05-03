import React, { Component } from 'react';
import { View, StyleSheet, FlatList, Image, TouchableHighlight, TouchableOpacity } from 'react-native';
import { Container, Header, Content, Card, CardItem, Thumbnail, Text, Button, Icon, Left, Body, Right } from 'native-base';

export default class MenuScreen extends Component {

  static navigationOptions = {
    headerTitle: "Công cụ",
    headerLeft: (
      <Left style={{ padding: 10 }}>
        <Thumbnail small source={{ uri: 'https://cdn1.vectorstock.com/i/1000x1000/27/40/setting-icon-vector-20142740.jpg' }} />
      </Left>
    ),

  };

  DATA = [
    {
      key: 'a', imageUri: 'http://icons.iconarchive.com/icons/custom-icon-design/flatastic-8/256/Home-icon.png'
    },
    {
      key: 'a1', imageUri: 'https://cdn3.iconfinder.com/data/icons/seo-glyph-2/24/gear-setting-512.png'
    },
    {
      key: 'a2', imageUri: 'https://cdn4.iconfinder.com/data/icons/small-n-flat/24/calendar-512.png'
    },
    {
      key: 'a3', imageUri: 'https://cdn3.iconfinder.com/data/icons/business-lifestyle/100/004-01-512.png'
    },
    // {
    //   key: 'a4', imageUri: 'http://icons.iconarchive.com/icons/custom-icon-design/flatastic-8/256/Home-icon.png'
    // },
    // {
    //   key: 'a5', imageUri: 'http://icons.iconarchive.com/icons/custom-icon-design/flatastic-8/256/Home-icon.png'
    // },
    // {
    //   key: 'a6', imageUri: 'http://icons.iconarchive.com/icons/custom-icon-design/flatastic-8/256/Home-icon.png'
    // },
    // {
    //   key: 'a7', imageUri: 'http://icons.iconarchive.com/icons/custom-icon-design/flatastic-8/256/Home-icon.png'
    // },
    // {
    //   key: 'a8', imageUri: 'http://icons.iconarchive.com/icons/custom-icon-design/flatastic-8/256/Home-icon.png'
    // }
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
      minheight: 80,
      maxHeight: 110,
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
