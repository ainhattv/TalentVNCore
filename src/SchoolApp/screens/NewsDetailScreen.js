import React from 'react';

import { Image, StyleSheet, ScrollView, RefreshControl } from 'react-native';
import { Container, Header, Content, Card, CardItem, Thumbnail, Text, Button, Icon, Left, Body, Right } from 'native-base';
import { createStackNavigator, createAppContainer } from 'react-navigation'; // Version can be specified in 

export default class NewsDetailScreen extends React.Component {

    API = "http://10.0.2.2:5001/api/v1";

    constructor(props) {
        super(props);

        newsID = this.props.navigation.getParam('NewsID', 'No ID');

        this.state = {
            loading: true,
            NewsID: newsID,
        }
    }

    async componentWillMount() {
        this.getNewsDetail(this.state.NewsID);

        this.setState({
            loading: false,
        });
    }

    static navigationOptions = ({ navigation }) => {

        return {
            title: navigation.getParam('NewsName', 'No Name'),
        };
    };

    _onRefresh = () => {
        this.setState({ refreshing: true });

        this.getNewsDetail(this.state.NewsID);

        this.setState({ refreshing: false });
    }

    async getNewsDetail(newsID) {
        PUSH_ENDPOINT = this.API + "/News/" + newsID;

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
                NewsData: data,
            });
        }
        catch (error) {
            console.error(error);
        };
    }

    render() {
        const { navigation } = this.props;

        if (this.state.loading) {
            return <Expo.AppLoading />;
        }

        if (this.state.NewsData) {
            const { NewsData } = this.state;
            return (
                <Container>
                    <ScrollView refreshControl={
                        <RefreshControl
                            refreshing={this.state.refreshing}
                            onRefresh={this._onRefresh}
                        />
                    } >
                        <Content>
                            <Card>
                                <CardItem>
                                    <Left>
                                        <Thumbnail source={{ uri: 'https://now.edu.vn/wp-content/uploads/2016/02/mixed-logo.jpg' }} />
                                        <Body>
                                            <Text>{NewsData.Name}</Text>
                                            <Text note>{NewsData.Header}</Text>
                                        </Body>
                                    </Left>
                                </CardItem>
                                <CardItem>

                                    <Image source={{ uri: NewsData.ImageUrl }} style={{ height: 300, width: null, flex: 1 }} />

                                </CardItem>

                                <CardItem>
                                    <Text>
                                        {NewsData.Body}
                                    </Text>
                                </CardItem>

                                <CardItem>
                                    <Left>
                                        <Button transparent >
                                            <Icon active name="thumbs-up" />
                                            <Text>12 View</Text>
                                        </Button>
                                    </Left>
                                    <Body>
                                        <Button transparent >
                                            <Icon active name="chatbubbles" />
                                            <Text>4 Comments</Text>
                                        </Button>
                                    </Body>
                                    <Right>
                                        <Text note>11h ago</Text>
                                    </Right>
                                </CardItem>
                            </Card>

                        </Content>
                    </ScrollView>
                </Container>
            );
        } else {
            return null;
        }

    }
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        paddingTop: 15,
        backgroundColor: '#fff',
    },
});
