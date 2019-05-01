import React from 'react';

import { Image, StyleSheet, ScrollView, RefreshControl } from 'react-native';
import { Container, Header, Content, Card, CardItem, Thumbnail, Text, Button, Icon, Left, Body, Right } from 'native-base';
import { createStackNavigator, createAppContainer } from 'react-navigation'; // Version can be specified in 

export default class NotificationDetailScreen extends React.Component {

    API = "http://10.0.2.2:5001/api/v1";

    constructor(props) {
        super(props);

        // Get navigate params
        notificationID = this.props.navigation.getParam('NotificationID', 'No ID');

        this.state = {
            loading: true,
            NotificationID: notificationID,
        }
    }

    async componentWillMount() {
        this.getNotificationDetail(this.state.NotificationID);

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

        this.getNotificationDetail(this.state.NotificationID);

        this.setState({ refreshing: false });
    }

    async getNotificationDetail(notificationID) {
        PUSH_ENDPOINT = this.API + "/Notifies/" + notificationID;

        try {
            const response = await fetch(PUSH_ENDPOINT, {
                method: 'GET',
                headers: {
                    Accept: 'application/json',
                    'Content-Type': 'application/json',
                },
            });

            let data = JSON.parse(response._bodyText);

            // console.log(data);

            this.setState({
                NotificationData: data,
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

        if (this.state.NotificationData) {
            const { NotificationData } = this.state;
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
                                        <Text>From: </Text>
                                    </Left>
                                    <Body>
                                        <Text>My School </Text>
                                    </Body>
                                </CardItem>
                                <CardItem>
                                    <Left>
                                        <Text>To: </Text>
                                    </Left>
                                    <Body>
                                        <Text>Van Nhat </Text>
                                    </Body>
                                </CardItem>
                                <CardItem>
                                    <Text>{NotificationData.Message}</Text>
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
