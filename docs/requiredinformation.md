# How to find required information

## Room Id

One way, there may be others, to find the unique Gitter Room Id is the following:

1. Open [Postman](https://www.getpostman.com/)
2. Create a new GET request, pointing at `https://api.gitter.im/v1/rooms/`
3. In the headers section, add a new header with `Content-Type` of `application\json`
4. In the headers section, add a new header with `Accept` of `application\json`
5. In the headers section, add a new header with `Authorization` of `Bearer <Personal Access Token`  **NOTE:** You can find your Personal Access Token by logging into [here](https://developer.gitter.im/apps)
6. Click Send

This will return a complete list of all the Rooms that you are a member of.  Scroll through the list until you find the one that you are interested, and take note of the Room Id, as shown in the picture below:

![2015-10-16_1236](https://cloud.githubusercontent.com/assets/1271146/10540744/b83d5110-7402-11e5-96bd-d7557da43ee6.png)

## Web Hook Url

Use the following steps to create a Custom Web Hook Url for your Gitter Room:

1. Log into gitter.im
2. Navigate to your Room
3. Click the Settings icon at the top of the page, and click Integrations
4. Scroll down to the "Add an integration" section, and click Custom
5. In the window that opens up you will see your newly created Web Hook Url.  It should look something like this

![2015-10-16_1240](https://cloud.githubusercontent.com/assets/1271146/10540791/31dc4814-7403-11e5-9f55-5f40dbbace84.png)

**NOTE:** The above Web Hook Url is a sample one, and no longer exists.

## Personal Access Token

In order to post directly into a Gitter Room, you need to use an Access Token.  Gitter does not provide the ability to generate Personal Access Tokens on a use case basis, however, you can use your own one.  You can retrieve that by doing the following:

1. Log into [https://developer.gitter.im/apps](https://developer.gitter.im/apps)
2. Your Personal Access Token will be displayed