import React, { Component } from 'react';
import { Image, FlatList, TouchableOpacity, Alert, ScrollView, RefreshControl } from 'react-native';
import { Container, Header, Content, Card, CardItem, Thumbnail, Text, Button, Icon, Left, Body, Right, Item } from 'native-base';
export default class NewsScreen extends Component {

    API = "http://10.0.2.2:5001/api/v1";

    constructor(props) {
        super(props);

        this.state = {
            loading: true,
        };

        this.getNewsList();
    }

    static navigationOptions = {
        headerTitle: "Bản tin",
        headerLeft: (
            <Left style={{ padding: 10 }}>
                <Thumbnail small source={{ uri: 'https://cdn1.iconfinder.com/data/icons/office-1/128/4-512.png' }} />
            </Left>
        ),

    };

    async componentWillMount() {
        await Expo.Font.loadAsync({
            Roboto: require("native-base/Fonts/Roboto.ttf"),
            Roboto_medium: require("native-base/Fonts/Roboto_medium.ttf"),
            Ionicons: require("@expo/vector-icons/fonts/Ionicons.ttf"),
        });

        this.setState({
            loading: false,
        });
    }

    _onRefresh = () => {
        this.setState({ refreshing: true });
        this.getNewsList();
        this.setState({ refreshing: false });
    }

    // Get data
    async getNewsList() {

        PUSH_ENDPOINT = this.API + "/News";

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

            this.setState({
                NewsList: data,
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

        if (this.state.NewsList) {
            return (
                <Container>
                    <ScrollView refreshControl={
                        <RefreshControl
                            refreshing={this.state.refreshing}
                            onRefresh={this._onRefresh}
                        />
                    } >

                        <Content>
                            {
                                this.state.NewsList.map((item, index) => {
                                    return (
                                        <Card key={index} style={{ flex: 0 }}>
                                            <CardItem>
                                                <Left>
                                                    <Thumbnail source={{ uri: item.ImageUrl }} />
                                                    <Body>
                                                        <Text>NativeBase</Text>
                                                        <Text note>April 15, 2016</Text>
                                                    </Body>
                                                </Left>
                                            </CardItem>
                                            <TouchableOpacity onPress={() => { navigate("NewsDetail", { NewsID: item.NewsID, NewsName: item.Name }) }}>
                                                <CardItem>
                                                    <Left>
                                                        <Image source={{ uri: item.ImageUrl }} style={{ height: 200, flex: 1, margin: 5 }} />
                                                    </Left>
                                                    <Right>
                                                        <Text note>
                                                            {item.Body}
                                                        </Text>
                                                    </Right>


                                                </CardItem>
                                            </TouchableOpacity>
                                            <CardItem>
                                                <Left>
                                                    <Button transparent >
                                                        <Icon active name="thumbs-up" />
                                                        <Text>12 View</Text>
                                                    </Button>
                                                </Left>
                                                {/* <Body>
                                                    <Button transparent >
                                                        <Icon active name="chatbubbles" />
                                                        <Text>4 Comments</Text>
                                                    </Button>
                                                </Body> */}
                                                <Right>
                                                    <Text note>11h ago</Text>
                                                </Right>
                                            </CardItem>
                                        </Card>
                                    )
                                })
                            }
                        </Content>

                    </ScrollView>
                </Container>
            );
        } else {
            return <Expo.AppLoading />;
        }

    }
}