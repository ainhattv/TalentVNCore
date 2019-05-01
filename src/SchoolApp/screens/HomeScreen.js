import React from 'react';
import {
  Image,
  Platform,
  ScrollView,
  StyleSheet,
  TouchableOpacity,
  View,
} from 'react-native';
import { Text, Container, Header, Left, Right, Button, Grid, Row, Col, Card, CardItem, Title, Icon, Body, Content, Accordion } from "native-base";
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
  };

  constructor(props) {
    super(props);

    this.state = {
      loading: true,
    };
  }

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
                  <Text>Mổ xẻ Galaxy Fold, iFixit phát hiện một lỗi thiết kế nghiêm trọng gây ra việc đột tử</Text>
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
                <Card>
                  <CardItem>
                    <Text>Mạng viễn thông Đông Dương ITelecom ra mắt: Dùng chung hạ tầng VinaPhone, 77.000 đồng được 90GB data/tháng, đầu số 087</Text>
                  </CardItem>
                  <CardItem cardBody>
                    <Image source={{ uri: 'http://genknews.genkcdn.vn/thumb_w/660/2019/4/25/img20190425095814-1556166807715832711308.jpg' }} style={{ height: 200, width: 500, flex: 1 }} />
                  </CardItem>
                  <CardItem>
                    <Text note>Ngày hôm nay, công ty Cổ phần Viễn thông Đông Dương Telecom (Indochina Telecom) đã chính thức ra mắt mạng di động ITelecom với đầu số 087 nhằm mang đến khách hàng dịch vụ viễn thông linh hoạt với chất lượng ổn định và chi phí hợp lý.</Text>
                  </CardItem>
                </Card>
              </Col>
              <Col>
                <Card>
                  <CardItem>
                    <Text>https://cdn.pixabay.com/photo/2013/04/06/11/50/ihttps://cdn.pixabay.com/photo/2013/04/06/11/50/i</Text>
                  </CardItem>
                  <CardItem cardBody>
                    <Image source={{ uri: 'https://cdn.pixabay.com/photo/2013/04/06/11/50/image-editing-101040_960_720.jpg' }} style={{ height: 200, width: 500, flex: 1 }} />
                  </CardItem>
                  <CardItem>
                    <Text>https://cdn.pixabay.com/photo/2013/04/06/11/50/ihttps://cdn.pixabay.com/photo/2013/04/06/11/50/i</Text>
                  </CardItem>
                </Card>
              </Col>
            </Row>
          </Grid>
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
  }
});
