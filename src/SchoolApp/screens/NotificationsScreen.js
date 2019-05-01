import React, { Component } from 'react';
import { Image, FlatList, TouchableOpacity, Alert, ScrollView, RefreshControl, StyleSheet } from 'react-native';
import { Icon, Badge, Container, Header, Content, List, ListItem, Thumbnail, Text, Left, Body, Right, Button } from 'native-base';
export default class NotificationsScreen extends Component {

  API = "http://10.0.2.2:5001/api/v1";

  constructor(props) {
    super(props);
    this.state = { loading: true };
  }

  static navigationOptions = {
    headerTitle: "Thông báo",
    headerLeft: (
      <Left style={{ padding: 10 }}>
        <Thumbnail small source={{ uri: 'https://cdn1.iconfinder.com/data/icons/office-1/128/4-512.png' }} />
      </Left>
    ),
  }

  async componentWillMount() {
    await Expo.Font.loadAsync({
      Roboto: require("native-base/Fonts/Roboto.ttf"),
      Roboto_medium: require("native-base/Fonts/Roboto_medium.ttf"),
      Ionicons: require("@expo/vector-icons/fonts/Ionicons.ttf"),
    });

    this.getNotificationList();

    this.setState({ loading: false });
  }

  _onRefresh = () => {
    this.setState({ refreshing: true });
    this.getNotificationList();
    this.setState({ refreshing: false });
  }

  // Get data
  async getNotificationList() {

    PUSH_ENDPOINT = this.API + "/Notifies";

    // // POST the token to your backend server from where you can retrieve it to send push notifications.
    try {
      const response = await fetch(PUSH_ENDPOINT, {
        method: 'GET',
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json',
        },
      });

      let data = JSON.parse(response._bodyText);

      console.log(data);

      this.setState({
        NotificationList: data,
      });
    }
    catch (error) {
      console.error(error);
    };

  }

  render() {
    const { navigate } = this.props.navigation;

    if (this.state.loading) {
      return <Expo.AppLoading />;
    }

    if (this.state.NotificationList) {
      return (
        <Container>
          <ScrollView refreshControl={
            <RefreshControl
              refreshing={this.state.refreshing}
              onRefresh={this._onRefresh}
            />
          } >
            <Content>
              <List>
                {
                  this.state.NotificationList.map((item, index) => {
                    return (

                      <ListItem key={index} thumbnail>
                        <Left>
                          <Thumbnail square source={{ uri: 'https://cdn1.iconfinder.com/data/icons/office-1/128/4-512.png' }} />
                        </Left>
                        <Body>
                          <Text>Sankhadeep</Text>
                          <Text note numberOfLines={1}>{item.Message}</Text>
                          <Text note >{item.CreatedDate}</Text>
                        </Body>
                        <Right>
                          <Button onPress={() => { navigate("NotificationDetail", { NotificationID: item.NotifyID }) }} transparent>
                            <Text>View</Text>
                          </Button>
                        </Right>
                      </ListItem>

                    )
                  })
                }
              </List>
            </Content>
          </ScrollView>
        </Container>
      );
    } else {
      return <Expo.AppLoading />;
    }
  }
}