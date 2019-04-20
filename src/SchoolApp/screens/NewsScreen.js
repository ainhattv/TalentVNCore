import React, { Component } from 'react';
import { Image } from 'react-native';
import { Container, Header, Content, Card, CardItem, Thumbnail, Text, Button, Icon, Left, Body, Right } from 'native-base';
export default class NewsScreen extends Component {

    constructor(props) {
        super(props);
        this.state = { loading: true };
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
        this.setState({ loading: false });
    }

    render() {

        const {navigate} = this.props.navigation;

        if (this.state.loading) {
            return <Expo.AppLoading />;
        }

        return (
            <Container>
                <Content>
                    <Card>
                        <CardItem>
                            <Left>
                                <Thumbnail source={{ uri: 'https://now.edu.vn/wp-content/uploads/2016/02/mixed-logo.jpg' }} />
                                <Body>
                                    <Text>NativeBase</Text>
                                    <Text note>GeekyAnts</Text>
                                </Body>
                            </Left>
                        </CardItem>
                        <CardItem button onPress={() => navigate('NewsDetail', {viewtitle: 'NativeBase title'})}>

                            <Image source={{ uri: 'https://static.foxnews.com/static/orion/styles/img/fox-news/og/og-fox-news.png' }} style={{ height: 200, width: null, flex: 1 }} />

                            <Text note>
                            //Your text here
                            </Text>

                        </CardItem>
                        <CardItem>
                            <Left>
                                <Button onPress={() => navigate('NewsDetail', {viewtitle: 'NativeBase title'})} transparent >
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

                    <Card style={{ flex: 0 }}>
                        <CardItem>
                            <Left>
                                <Thumbnail source={{ uri: 'https://now.edu.vn/wp-content/uploads/2016/02/mixed-logo.jpg' }} />
                                <Body>
                                    <Text>NativeBase</Text>
                                    <Text note>April 15, 2016</Text>
                                </Body>
                            </Left>
                        </CardItem>
                        <CardItem>
                            <Image source={{ uri: 'https://static.foxnews.com/static/orion/styles/img/fox-news/og/og-fox-news.png' }} style={{ height: 200, width: null, flex: 1 }} />

                            <Text note>
                            //Your text here
                            </Text>

                        </CardItem>
                        <CardItem>
                            <Right>
                                <Button transparent textStyle={{ color: '#87838B' }}>
                                    <Icon name="logo-github" />
                                    <Text>1,926 stars</Text>
                                </Button>
                            </Right>
                        </CardItem>
                    </Card>

                    <Card>
                        <CardItem>
                            <Left>
                                <Thumbnail source={{ uri: 'https://now.edu.vn/wp-content/uploads/2016/02/mixed-logo.jpg' }} />
                                <Body>
                                    <Text>NativeBase</Text>
                                    <Text note>GeekyAnts</Text>
                                </Body>
                            </Left>
                        </CardItem>
                        <CardItem>

                            <Image source={{ uri: 'https://static.foxnews.com/static/orion/styles/img/fox-news/og/og-fox-news.png' }} style={{ height: 200, width: null, flex: 1 }} />

                            <Text note>
                            //Your text here
                            </Text>

                        </CardItem>
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

                    <Card style={{ flex: 0 }}>
                        <CardItem>
                            <Left>
                                <Thumbnail source={{ uri: 'https://now.edu.vn/wp-content/uploads/2016/02/mixed-logo.jpg' }} />
                                <Body>
                                    <Text>NativeBase</Text>
                                    <Text note>April 15, 2016</Text>
                                </Body>
                            </Left>
                        </CardItem>
                        <CardItem>
                            <Image source={{ uri: 'https://static.foxnews.com/static/orion/styles/img/fox-news/og/og-fox-news.png' }} style={{ height: 200, width: null, flex: 1 }} />

                            <Text note>
                            //Your text here
                            </Text>

                        </CardItem>
                        <CardItem>
                            <Right>
                                <Button transparent textStyle={{ color: '#87838B' }}>
                                    <Icon name="logo-github" />
                                    <Text>1,926 stars</Text>
                                </Button>
                            </Right>
                        </CardItem>
                    </Card>

                    <Card>
                        <CardItem>
                            <Left>
                                <Thumbnail source={{ uri: 'https://now.edu.vn/wp-content/uploads/2016/02/mixed-logo.jpg' }} />
                                <Body>
                                    <Text>NativeBase</Text>
                                    <Text note>GeekyAnts</Text>
                                </Body>
                            </Left>
                        </CardItem>
                        <CardItem>

                            <Image source={{ uri: 'https://static.foxnews.com/static/orion/styles/img/fox-news/og/og-fox-news.png' }} style={{ height: 200, width: null, flex: 1 }} />

                            <Text note>
                            //Your text here
                            </Text>

                        </CardItem>
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

                    <Card style={{ flex: 0 }}>
                        <CardItem>
                            <Left>
                                <Thumbnail source={{ uri: 'https://now.edu.vn/wp-content/uploads/2016/02/mixed-logo.jpg' }} />
                                <Body>
                                    <Text>NativeBase</Text>
                                    <Text note>April 15, 2016</Text>
                                </Body>
                            </Left>
                        </CardItem>
                        <CardItem>
                            <Image source={{ uri: 'https://static.foxnews.com/static/orion/styles/img/fox-news/og/og-fox-news.png' }} style={{ height: 200, width: null, flex: 1 }} />

                            <Text note>
                            //Your text here
                            </Text>

                        </CardItem>
                        <CardItem>
                            <Right>
                                <Button transparent textStyle={{ color: '#87838B' }}>
                                    <Icon name="logo-github" />
                                    <Text>1,926 stars</Text>
                                </Button>
                            </Right>
                        </CardItem>
                    </Card>
                </Content>
            </Container>
        );
    }
}