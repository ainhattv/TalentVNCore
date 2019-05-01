export default class NewsService {

    API = "http://10.0.2.2:5001/api/v1";

    getNewsList = async () => {

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
            const responseJson = data;
        }
        catch (error) {
            console.error(error);
        };

    }
}