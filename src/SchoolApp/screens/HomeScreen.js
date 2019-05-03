import React from 'react';
import {
  Image,
  Platform,
  ScrollView,
  StyleSheet,
  TouchableOpacity,
  View,
} from 'react-native';
import { List, ListItem, Text, Thumbnail, Container, Header, Left, Right, Button, Grid, Row, Col, Card, CardItem, Title, Icon, Body, Content, Accordion } from "native-base";
import { WebBrowser } from 'expo';

export default class HomeScreen extends React.Component {
  static navigationOptions = {
    title: 'Home',
    headerStyle: {
      backgroundColor: '#4d94ff',
    },
    headerTintColor: '#fff',
    headerTitleStyle: {
      fontWeight: 'bold',
    },
    headerRight: (
      <Button transparent>
        <Icon name="search" />
      </Button>
    ),
  };

  constructor(props) {
    super(props);

    this.state = {
      loading: true,
    };
  }

  datas = [
    {
      text: "Sankhadeep: Bí quyết có tấm hình đẹp đa góc độ",
      note: "Its time to build a difference . ."
    },
    {
      text: "Supriya: Bí quyết có tấm hình đẹp đa góc độ",
      note: "One needs courage to be happy and smiling all time . . "
    },
    {
      text: "Shivraj: Bí quyết có tấm hình đẹp đa góc độ",
      note: "Time changes everything . ."
    },
    {
      text: "Shruti: Bí quyết có tấm hình đẹp đa góc độ",
      note: "The biggest risk is a missed opportunity !!"
    },
    {
      text: "Himanshu: Bí quyết có tấm hình đẹp đa góc độ",
      note: "Live a life style that matchs your vision"
    },
    {
      text: "Shweta: Bí quyết có tấm hình đẹp đa góc độ",
      note: "Failure is temporary, giving up makes it permanent"
    }
  ];

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

  render() {

    if (this.state.loading) {
      return <Expo.AppLoading />;
    }

    return (
      <View>
        <ScrollView>
          <Grid>
            <Row>
              <Card>
                <CardItem>
                  <Text style={styles.titleText}>Mổ xẻ Galaxy Fold, iFixit phát hiện một lỗi thiết kế nghiêm trọng gây ra việc đột tử</Text>
                </CardItem>
                <CardItem cardBody>
                  <Image source={{ uri: 'https://cdn.pixabay.com/photo/2013/04/06/11/50/image-editing-101040_960_720.jpg' }} style={{ height: 200, width: 500, flex: 1 }} />
                </CardItem>
                <CardItem>
                  <Text note>Trong khi Samsung quá tập trung vào hệ thống bản lề chắc chắn của Galaxy Fold, phần màn hình lại không được bảo vệ hoàn toàn trước bụi bẩn.</Text>
                </CardItem>
              </Card>
            </Row>
            <Row  >
              <Col>
                <Card style={styles.secondItem}>
                  <CardItem>
                    <Text style={styles.titleText} numberOfLines={2}>Mạng viễn thông Đông Dương ITelecom ra mắt: Dùng chung hạ tầng VinaPhone, 77.000 đồng được 90GB data/tháng, đầu số 087</Text>
                  </CardItem>
                  <CardItem cardBody>
                    <Image source={{ uri: 'http://genknews.genkcdn.vn/thumb_w/660/2019/4/25/img20190425095814-1556166807715832711308.jpg' }} style={{ height: 150, width: 200, flex: 1 }} />
                  </CardItem>
                </Card>
              </Col>
              <Col>
                <Card style={styles.secondItem}>
                  <CardItem>
                    <Text style={styles.titleText} numberOfLines={2}>Mạng viễn thông Đông Dương ITelecom ra mắt: Dùng chung hạ tầng VinaPhone, 77.000 đồng được 90GB data/tháng, đầu số 087</Text>
                  </CardItem>
                  <CardItem cardBody>
                    <Image source={{ uri: 'https://cdn.pixabay.com/photo/2013/04/06/11/50/image-editing-101040_960_720.jpg' }} style={{ height: 150, width: 500, flex: 1 }} />
                  </CardItem>
                </Card>
              </Col>
            </Row>
          </Grid>
          <Card>
            <List
              dataArray={this.datas}
              renderRow={data =>
                <ListItem thumbnail style={styles.listItem}>
                  <Left>
                    <Thumbnail square large source={{ uri: 'https://znews-photo.zadn.vn/w660/Uploaded/wyhktpu/2018_11_22/image003_4.jpg' }} />
                  </Left>
                  <Body>
                    <Text>
                      {data.text}
                    </Text>
                    <Text numberOfLines={3} note>
                      {data.note}
                    </Text>
                  </Body>
                </ListItem>}
            />
          </Card>
        </ScrollView>
      </View>
    );
  }
}

const styles = StyleSheet.create({
  container: {
    backgroundColor: '#fff',
  },
  header: {
    flex: 1,
    backgroundColor: '#3399ff',
  },
  secondItem: {
  },
  titleText: {
    fontSize: 14,
    fontWeight: 'bold',
  },
  bodyText: {
    fontSize: 14,
  },
  thirdImage: {
    height: 20,
    width: 20
  },
  listItem: {
    padding: 3
  }
});
