import { Permissions, Notifications } from 'expo';
import { Platform, StatusBar, StyleSheet, View, Alert } from 'react-native';

const PUSH_ENDPOINT = 'https://schoolcms20190420070833.azurewebsites.net/api/v1/Notifies/RegisterNotify';
// const PUSH_ENDPOINT = 'http://10.0.2.2:5001/api/v1/Notifies/RegisterNotify';

export async function registerForPushNotificationsAsync() {
  const { status: existingStatus } = await Permissions.getAsync(
    Permissions.NOTIFICATIONS
  );
  let finalStatus = existingStatus;

  // only ask if permissions have not already been determined, because
  // iOS won't necessarily prompt the user a second time.
  if (existingStatus !== 'granted') {
    // Android remote notification permissions are granted during the app
    // install, so this will only ask on iOS
    const { status } = await Permissions.askAsync(Permissions.NOTIFICATIONS);
    finalStatus = status;
  }

  // Stop here if the user did not grant permissions
  if (finalStatus !== 'granted') {
    return;
  }

  // Get the token that uniquely identifies this device
  let token = await Notifications.getExpoPushTokenAsync();

  console.log(token);

  Alert.alert(token);

  // // POST the token to your backend server from where you can retrieve it to send push notifications.
  return fetch(PUSH_ENDPOINT, {
    method: 'POST',
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({
      AccountID: "123",
      DeviceToken: token,
    }),
  })
    .then((response) => {
      console.log(JSON.parse(response._bodyText))
    })
    .then((responseJson) => {
      //console.log(responseJson.DeviceToken)
    })
    .catch((error) => {
      console.error(error);
    });
  ;
}