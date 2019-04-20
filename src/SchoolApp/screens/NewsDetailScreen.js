import React from 'react';

import { Image, StyleSheet } from 'react-native';
import { Container, Header, Content, Card, CardItem, Thumbnail, Text, Button, Icon, Left, Body, Right } from 'native-base';
import { createStackNavigator, createAppContainer } from 'react-navigation'; // Version can be specified in 

export default class NewsDetailScreen extends React.Component {

    constructor(props) {
        super(props);
    }

    async componentWillMount() {
        
    }

    static navigationOptions = ({ navigation }) => {
        return {
            title: navigation.getParam('viewtitle', 'A Nested Details Screen'),
        };
    };

    render() {
        const { navigation } = this.props;
        const otherParam = navigation.getParam('viewtitle', 'A Nested Details Screen');
        return (
            <Container>
                <Content>
                    <Card>
                        <CardItem>
                            <Left>
                                <Thumbnail source={{ uri: 'https://now.edu.vn/wp-content/uploads/2016/02/mixed-logo.jpg' }} />
                                <Body>
                                    <Text>NativeBase</Text>
                                    <Text note>{JSON.stringify(otherParam)}</Text>
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

                </Content>
            </Container>
        );
    }
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        paddingTop: 15,
        backgroundColor: '#fff',
    },
});
