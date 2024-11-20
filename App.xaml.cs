
using DMS.CustomControls;
using DMS.Dtos;
using DMS.Models;
using DMS.Util;
using DMS.ViewModel;
using DMS.Views;
using Newtonsoft.Json;
using Plugin.Firebase.CloudMessaging;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using DMS.ViewModel.Base;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Platform;
#if __ANDROID__
using Android.Content.Res;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
#endif

namespace DMS;

public partial class App : Application
{
    // public ApiRest Rest = new ApiRest();
    public DeviceStorage st = new DeviceStorage();
    string token;
    public AppThemeColorsDto AppThemeColors;
    public BeforeLoginDefaultColorThemeDto BeforeLoginDefaultColors;
    public NotificationDto NotificationObject;

    public App()
    {

        InitializeComponent();

        AppThemeColors = ContentJsonSerializer.JsonDeserialize<AppThemeColorsDto>(Preferences.Get("AppThemeColors", null));

        if (AppThemeColors != null)
        {

            Microsoft.Maui.Controls.Application.Current.Resources["mainColor"] = Color.FromHex(AppThemeColors.mainColor);
            Microsoft.Maui.Controls.Application.Current.Resources["MainColor"] = Color.FromHex(AppThemeColors.mainColor);
            Microsoft.Maui.Controls.Application.Current.Resources["LineColor"] = Color.FromHex(AppThemeColors.LineColor);
            Microsoft.Maui.Controls.Application.Current.Resources["FrameBackColor"] = Color.FromHex(AppThemeColors.FrameBackColor);
            Microsoft.Maui.Controls.Application.Current.Resources["BackDrop"] = Color.FromHex(AppThemeColors.BackDrop);
            Microsoft.Maui.Controls.Application.Current.Resources["entryBorderColor"] = Color.FromHex(AppThemeColors.entryBorderColor);
            Microsoft.Maui.Controls.Application.Current.Resources["SeperatorGreyColor"] = Color.FromHex(AppThemeColors.SeperatorGreyColor);
            Microsoft.Maui.Controls.Application.Current.Resources["DefaultBlack"] = Color.FromHex(AppThemeColors.DefaultBlack);
            Microsoft.Maui.Controls.Application.Current.Resources["DefaultLightGrey"] = Color.FromHex(AppThemeColors.DefaultLightGrey);
            Microsoft.Maui.Controls.Application.Current.Resources["BorderColor"] = Color.FromHex(AppThemeColors.BorderColor);
            Microsoft.Maui.Controls.Application.Current.Resources["labelheading"] = Color.FromHex(AppThemeColors.labelheading);
            Microsoft.Maui.Controls.Application.Current.Resources["labelValue"] = Color.FromHex(AppThemeColors.labelValue);
            Microsoft.Maui.Controls.Application.Current.Resources["labelValue2"] = Color.FromHex(AppThemeColors.labelValue2);
            Microsoft.Maui.Controls.Application.Current.Resources["TextColor"] = Color.FromHex(AppThemeColors.TextColor);
            Microsoft.Maui.Controls.Application.Current.Resources["smallSubHeadingGreyLabel"] = Color.FromHex(AppThemeColors.smallSubHeadingGreyLabel);
            Microsoft.Maui.Controls.Application.Current.Resources["FrameBackgroundWhiteColor"] = Color.FromHex(AppThemeColors.FrameBackgroundWhiteColor);
            Microsoft.Maui.Controls.Application.Current.Resources["ProductCardFlexColor"] = Color.FromHex(AppThemeColors.ProductCardFlexColor);
            Microsoft.Maui.Controls.Application.Current.Resources["VerticalSeperatorColor"] = Color.FromHex(AppThemeColors.VerticalSeperatorColor);
            Microsoft.Maui.Controls.Application.Current.Resources["SearchBack"] = Color.FromHex(AppThemeColors.SearchBack);
            Microsoft.Maui.Controls.Application.Current.Resources["SearchBackground"] = Color.FromHex(AppThemeColors.SearchBackground);
            Microsoft.Maui.Controls.Application.Current.Resources["HeadingLightGrey"] = Color.FromHex(AppThemeColors.HeadingLightGrey);
            Microsoft.Maui.Controls.Application.Current.Resources["whiteColor"] = Color.FromHex(AppThemeColors.whiteColor);
            Microsoft.Maui.Controls.Application.Current.Resources["bgGrayColor"] = Color.FromHex(AppThemeColors.bgGrayColor);
            Microsoft.Maui.Controls.Application.Current.Resources["labelTextColor"] = Color.FromHex(AppThemeColors.labelTextColor);
            Microsoft.Maui.Controls.Application.Current.Resources["PageTitleColor"] = Color.FromHex(AppThemeColors.PageTitleColor);
            Microsoft.Maui.Controls.Application.Current.Resources["GrayColor"] = Color.FromHex(AppThemeColors.GrayColor);
            Microsoft.Maui.Controls.Application.Current.Resources["labelEntryValueTextColor"] = Color.FromHex(AppThemeColors.labelEntryValueTextColor);
            Microsoft.Maui.Controls.Application.Current.Resources["labelEntryHeadingTextColor"] = Color.FromHex(AppThemeColors.labelEntryHeadingTextColor);
            Microsoft.Maui.Controls.Application.Current.Resources["LoaderColor"] = Color.FromHex(AppThemeColors.LoaderColor);
            Microsoft.Maui.Controls.Application.Current.Resources["Entrybackground"] = Color.FromHex(AppThemeColors.Entrybackground);
        }
        if (BeforeLoginDefaultColors != null)
        {
            BeforeLoginDefaultColors.MainBlueBaseColor = "#FF0000";
            Microsoft.Maui.Controls.Application.Current.Resources["LabelDarkGrayBaseColor"] = Color.FromHex(BeforeLoginDefaultColors.LabelDarkGrayBaseColor);
            Microsoft.Maui.Controls.Application.Current.Resources["WhiteBaseColor"] = Color.FromHex(BeforeLoginDefaultColors.WhiteBaseColor);
            Microsoft.Maui.Controls.Application.Current.Resources["MainBlueBaseColor"] = Color.FromHex(BeforeLoginDefaultColors.MainBlueBaseColor);
            Microsoft.Maui.Controls.Application.Current.Resources["NoAccLabelBaseColor"] = Color.FromHex(BeforeLoginDefaultColors.NoAccLabelBaseColor);
            Microsoft.Maui.Controls.Application.Current.Resources["BackDropBaseColor"] = Color.FromHex(BeforeLoginDefaultColors.BackDropBaseColor);
            Microsoft.Maui.Controls.Application.Current.Resources["ListSepratorBaseColor"] = Color.FromHex(BeforeLoginDefaultColors.ListSepratorBaseColor);
            Microsoft.Maui.Controls.Application.Current.Resources["LightGrayBaseColor"] = Color.FromHex(BeforeLoginDefaultColors.LightGrayBaseColor);
            Microsoft.Maui.Controls.Application.Current.Resources["RedStarBaseColor"] = Color.FromHex(BeforeLoginDefaultColors.RedStarBaseColor);
            Microsoft.Maui.Controls.Application.Current.Resources["FrameBorderBaseColor"] = Color.FromHex(BeforeLoginDefaultColors.FrameBorderBaseColor);
        }

        //entry control to remove underline
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(BorderLessEntry),
        (handler, view) =>
        {
            if (view is BorderLessEntry)
            {
#if ANDROID
                handler.PlatformView.SetBackgroundColor(Colors.Transparent.ToAndroid());
#elif IOS
                handler.PlatformView.BackgroundColor = Colors.Transparent.ToPlatform();
                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#elif WINDOWS
                //  handler.NativeView.Background = Colors.Transparent.ToNative();
#endif
            }


        });
        Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping(nameof(BorderLessEditor),
       (handler, view) =>
       {
           if (view is BorderLessEditor)
           {
#if ANDROID
               handler.PlatformView.SetBackgroundColor(Colors.Transparent.ToAndroid());
#elif IOS
               //handler.PlatformView.BackgroundColor = Colors.Transparent.ToPlatform();
               // handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#elif WINDOWS
                //  handler.NativeView.Background = Colors.Transparent.ToNative();
#endif
           }


       });
        //datepicker
        Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping(nameof(CustomDatePicker), (handler, view) =>
       {

           if (view is CustomDatePicker)
           {
#if ANDROID
            
               handler.PlatformView.SetBackgroundColor(Colors.Transparent.ToAndroid());
#elif IOS
               handler.PlatformView.BackgroundColor = Colors.Transparent.ToPlatform();
               handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
           }
       });


        Instances.Instance.UserToken = ContentJsonSerializer.JsonDeserialize<LoginResponseDto>(Preferences.Get("token", null));
        Instances.Instance.UserDetails = ContentJsonSerializer.JsonDeserialize<ProfileDto>(Preferences.Get("ProfileDetails", null));
        Instances.Instance.TenantDetails = ContentJsonSerializer.JsonDeserialize<TenantDetailsDto>(Preferences.Get("TenantDetails", null));

        //Navigate to Second Splash Screen

        MainPage = new SplashScreen2Viewpage();

        System.Globalization.CultureInfo customCulture = new System.Globalization.CultureInfo("en-US", true);
        customCulture.DateTimeFormat.ShortDatePattern = "dd-MMM-yyyy";
        System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
        System.Threading.Thread.CurrentThread.CurrentUICulture = customCulture;
    }



    protected override async void OnStart()
    {
        //if (Instances.Instance.DeepLink != null&& !Instances.Instance.IsDeepLinkRestricted)
        //{
        //    dynamic deepLink =Instances.Instance.DeepLink;
        //    var PageName = deepLink.GetQueryParameter("page");
        //    var Id = deepLink.GetQueryParameter("id");
        //    Instances.Instance.IsDeepLinkRestricted = true;
        //}
        await Task.Delay(500);
        if (Instances.Instance.IsWhitelabelled == true || Instances.Instance.UserToken != null)
            await GetTenantDetails();

        if (Instances.Instance.UserToken == null)
        {
            MainPage = new NavigationPage(new WelcomeVerificationPage());
        }
        else
        {
            if (Instances.Instance.UserDetails != null && Instances.Instance.UserDetails.isVerified)
            {
                //// Create the loader page
                //var loaderPage = new LoaderPage();
                //Application.Current.MainPage = new NavigationPage(loaderPage);

                //// Simulate some initialization delay
                //// You can replace this with your actual initialization code
                //Device.StartTimer(TimeSpan.FromSeconds(3), () =>
                //{
                //    // Once the initialization is done, set the AppShell as the MainPage
                Application.Current.MainPage = new AppShell();

                //    // Return false to stop the timer
                //    return false;
                //});

                //MainPage = new NavigationPage(new CreateBrandActivitypage());
            }
            else
            {
                //MainPage = new AppShell();
                MainPage = new NavigationPage(new VerifyInformationPage());// new VerifyInformationPage();
            }

            var Storedjson = Preferences.Get("ResourceJson", null);

            if (!string.IsNullOrEmpty(Storedjson))
            {
                var dictionary = JsonConvert.DeserializeObject<List<Text>>(Storedjson);
                Instances.Instance.LanguageConvertList = dictionary;
            }
        }

        // await fetchdetail();
        InitializePushNotificationAsync();

        if (Instances.Instance.UserToken != null)
        {
            GetStatusColor();
            await GetProfileAPICall();

        }
        OnResume();
    }
    //protected override async void OnResume()
    //{
    //    if (Instances.Instance.DeepLink !=null && !Instances.Instance.IsDeepLinkRestricted)
    //    {
    //        dynamic deepLink = Instances.Instance.DeepLink;
    //        var PageName = deepLink.GetQueryParameter("page");
    //        var Id = deepLink.GetQueryParameter("id");
    //        Instances.Instance.IsDeepLinkRestricted = true;
    //    }
    //}

    protected override void OnSleep()
    {
        // Handle when your app sleeps  
    }

    private async Task InitializePushNotificationAsync()
    {
        try
        {
            Instances.Instance.DeviceDetail = new DeviceDto();
            Instances.Instance.DeviceDetail.callPoint = "APP";
            Instances.Instance.DeviceDetail.deviceModel = DeviceInfo.Manufacturer + " " + DeviceInfo.Model;
            Instances.Instance.DeviceDetail.deviceId = DeviceInfo.Name;
            Instances.Instance.DeviceDetail.deviceOS = DeviceInfo.Platform.ToString();
            Instances.Instance.DeviceDetail.deviceVersion = DeviceInfo.Version.ToString();
            Instances.Instance.DeviceDetail.appVersion = AppInfo.VersionString;
            Instances.Instance.DeviceDetail.deviceType = DeviceInfo.Platform.ToString();
            Instances.Instance.DeviceDetail.uuid = "0";
            Instances.Instance.DeviceDetail.userId = 0;
            Instances.Instance.DeviceDetail.id = 0;
            Instances.Instance.DeviceDetail.tenantId = 2;

            Instances.Instance.DeviceDetail.insertedDate = DateTime.Today;
            await CrossFirebaseCloudMessaging.Current.CheckIfValidAsync();
            var token = await CrossFirebaseCloudMessaging.Current.GetTokenAsync();
            Instances.Instance.DeviceDetail.uuid = token;

            CrossFirebaseCloudMessaging.Current.NotificationReceived += (s, p) =>
            {
                try
                {

                    //if (p.Notification.Data.Contains("id"))
                    //{
                    //    //MessagingCenter.Send(this, MessagingCenterKeys.NotificationRecieved);
                    //}
                }
                catch (Exception ex)
                {
                }
            };
            CrossFirebaseCloudMessaging.Current.NotificationTapped += (s, p) =>
            {
                try
                {
                    NotificationOpenedAsync(p.Notification);

                }
                catch (Exception ex)
                {
                }
            };


        }
        catch (Exception ex)
        {

        }
        // await SendDeviceDetailsAsync();
    }
    private async void NotificationOpenedAsync(FCMNotification notification) //added by bhawna for notification redirection
    {
        var NotificationType = "";
        var Notificationid = "";
        // var data=notification.Data;
        foreach (var data in notification.Data)
        {
            try
            {

                //  dynamic finaldata = JsonConvert.DeserializeObject<object>(data.ToString());
                if (data.Key == "NotificationType")
                {
                    NotificationType = data.Value;
                }
                if (data.Key == "NotificationId")
                {
                    Notificationid = data.Value;
                }
            }
            catch (Exception e)
            {

            }

        }
        int id = 0;
        if (!string.IsNullOrEmpty(Notificationid))
        {
            await GetNotificationDetailById(Notificationid);

            if (NotificationObject != null)/*&& NotificationObject.result.NotificationType== "ORDER")*/
            {
                if (NotificationType.ToLower() == "order")
                {
                    id = Convert.ToInt32(NotificationObject.result.ReferenceId);
                    await Shell.Current.GoToAsync(nameof(MyOrderListPageView));
                    //  await Application.Current.MainPage.Navigation.PushAsync(new MyOrderListPageView());

                    MyOrderListPageViewModel vm = new MyOrderListPageViewModel();
                    if (id != 0)
                        vm.ViewOrderDetailPage(id, 0); //added Tarun@kansoft 17-06-2024 New Notification Routing ---------- ( #SR-3484 )

                }
                else if (NotificationType.ToLower() == "payment")
                {
                    id = Convert.ToInt32(NotificationObject.result.ReferenceId);
                    // await Application.Current.MainPage.Navigation.PushAsync(new PaymentSchedulingPageView());
                    await Shell.Current.GoToAsync(nameof(PaymentSchedulingPageView));
                    if (id != 0)
                    {
                        PaymentPlanningDto data = new PaymentPlanningDto();
                        data.id = id;
                        data.IsReschedule = true;
                        // await  Application.Current.MainPage.Navigation.PushAsync(new CreatePaymentSchedulePageView(data));
                        await Shell.Current.Navigation.PushAsync(new ReorderPaymentSchedulingPage(data));
                    }


                }
                //added Tarun@kansoft 17-06-2024 New Notification Routing ---------- START ( #SR-3484 )
                else if (NotificationType.ToLower() == "ordertimeline")
                {
                    id = Convert.ToInt32(NotificationObject.result.ReferenceId);
                    await Shell.Current.GoToAsync(nameof(MyOrderListPageView));
                    //  await Application.Current.MainPage.Navigation.PushAsync(new MyOrderListPageView());

                    MyOrderListPageViewModel vm = new MyOrderListPageViewModel();
                    if (id != 0)
                        vm.ViewOrderDetailPage(id, 1);
                }
                else if (NotificationType.ToLower() == "orderdispatch")
                {
                    id = Convert.ToInt32(NotificationObject.result.ReferenceId);
                    await Shell.Current.GoToAsync(nameof(MyOrderListPageView));
                    //  await Application.Current.MainPage.Navigation.PushAsync(new MyOrderListPageView());

                    MyOrderListPageViewModel vm = new MyOrderListPageViewModel();
                    if (id != 0)
                        vm.ViewOrderDetailPage(id, 2);
                }
                else if (NotificationType.ToLower() == "createPaymentScheduling")
                {
                    await Shell.Current.GoToAsync(nameof(PaymentSchedulingPageView));
                    PaymentSchedulingPageViewModel vm = new PaymentSchedulingPageViewModel();
                    vm.AddButtonClick();
                }
                else if (NotificationType.ToLower() == "paymentScheduling")
                {
                    id = Convert.ToInt32(NotificationObject.result.ReferenceId);
                    await Shell.Current.GoToAsync(nameof(PaymentSchedulingPageView));
                    PaymentSchedulingPageViewModel vm = new PaymentSchedulingPageViewModel();
                   
                }
                //added Tarun@kansoft 17-06-2024 New Notification Routing ---------- END ( #SR-3484 )
            }
        }





    }
    public static void PushingMenuPageToNavstack()
    {
        // Application.Current.MainPage.Navigation.InsertPageBefore(new MenuPage(), Application.Current.MainPage.Navigation.NavigationStack[0]);
        // Application.Current.MainPage.Navigation.PopToRootAsync(false);
    }
    public async Task GetNotificationDetailById(string id)
    {
        try
        {


            await Instances.Instance.rest.ExecuteRun<NotificationDto>("",
                GetNotificationDetailByIdCallBack, Constants.GetNotificationDetailById + "?NotificationId=" + id, HttpMethod.Get);
        }
        catch (Exception ex)
        {
            Instances.Instance.Log.Error(string.Format("GetStatusColor: {0}\n{1}\n{2} ", ex.Message));

        }

    }
    public void GetNotificationDetailByIdCallBack(RestResponse<NotificationDto> result)
    {
        try
        {
            if (result != null)
            {
                if (result.Data.result != null)
                {
                    NotificationObject = result.Data.result;
                }
            }
        }
        catch (Exception ex)
        {
            Instances.Instance.Log.Error(string.Format("GetStatusColor: {0}\n{1}\n{2} ", ex.Message));

        }
    }
    public async Task SendDeviceDetailsAsync()
    {
        try
        {
            string requestData = ContentJsonSerializer.JsonSerialize(Instances.Instance.DeviceDetail);
            await Instances.Instance.rest.ExecuteRun<DeviceDtoResponse>(requestData,
                 SendDeviceDetailscallBack, Constants.SendDeviceDetail, HttpMethod.Post);
        }
        catch (Exception ex)
        {
            Instances.Instance.Log.Error(string.Format("Login unsuccessful: {0}\n{1}\n{2} ", ex.Message));
        }
    }
    private void SendDeviceDetailscallBack(RestResponse<DeviceDtoResponse> result)
    {
        if (!result.Data.success)
        {
            Instances.Instance.Log.Error(string.Format("Login unsuccessful: {0}\n{1}\n{2} ", result.Data.errormessage));
        }
    }
    public static void GetStatusColor()
    {
        try
        {
            Instances.Instance.rest.ExecuteRun<ObservableCollection<StatusColorDto>>("",
               GetStatusColorCallBack, Constants.GetStatusColor, HttpMethod.Get);
        }
        catch (Exception ex)
        {
            Instances.Instance.Log.Error(string.Format("GetStatusColor: {0}\n{1}\n{2} ", ex.Message));

        }

    }
    public static void GetStatusColorCallBack(RestResponse<ObservableCollection<StatusColorDto>> result)
    {
        try
        {
            if (result != null)
            {
                if (result.Data.result != null)
                {
                    Instances.Instance.StatusColorList = result.Data.result;
                }
            }
        }
        catch (Exception ex)
        {
            Instances.Instance.Log.Error(string.Format("GetStatusColor: {0}\n{1}\n{2} ", ex.Message));

        }
    }

    public async Task GetProfileAPICall()
    {
        try
        {
            // json - request data,  callback function after getting response, service response , Api type
            await Instances.Instance.rest.ExecuteRun<ProfileDto>("", GetProfileAPICallback, Constants.GetProfile + "?CustomerUserId=" + Instances.Instance.UserToken.userId, HttpMethod.Get);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void GetProfileAPICallback(RestResponse<ProfileDto> Apiresult)
    {
        try
        {
            if (Apiresult.Data?.error != null && Apiresult.Data?.error?.message != null)
            {
                //Warn(Apiresult.Data.error.message, false);
            }

            else
            {
                if (Apiresult != null)
                {

                    string ProfileDetail = ContentJsonSerializer.JsonSerialize(Apiresult.Data.result);
                    Preferences.Set("ProfileDetails", ProfileDetail);


                }
            }
        }
        catch (Exception ex)
        {
            // Warn(ex.Message, false);
        }
    }

    public static async Task GetTenantDetails()
    {
        try
        {
            //  ApiRest rest = new ApiRest();

            await Instances.Instance.rest.ExecuteRun<TenantDetailsDto>("",
               GetTenantDetailsCallBack, Constants.GetTenantDetails + "?TenancyName=" + Instances.Instance.TenancyName + "&URL", HttpMethod.Get);
        }
        catch (Exception ex)
        {
            //Instances.Instance.Log.Error(string.Format("GetStatusColor: {0}\n{1}\n{2} ", ex.Message));
            await App.Current.MainPage.DisplayAlert("Alert", "Check your network connection and try again", "ok");
        }

    }
    public async static void GetTenantDetailsCallBack(RestResponse<TenantDetailsDto> result)
    {
        try
        {
            if (result != null)
            {
                if (result.Data != null)
                {
                    if (result.Data?.result != null)
                    {
                        string base64 = System.Convert.ToBase64String(result.Data.result.bytes);
                        ImageSource img = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(base64)));

                        Instances.Instance.TenantDetails = result.Data.result;


                        Instances.Instance.FileLink = result.Data.result.imageUrl;
                        string TenantDetail = ContentJsonSerializer.JsonSerialize(result.Data.result);
                        Preferences.Set("TenantDetails", TenantDetail);
                        Instances.Instance.TenantDetails.Logo = img;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            await App.Current.MainPage.DisplayAlert("Alert", "Something went wrong, check your Internet connectivity or contact Admin", "ok");
            Instances.Instance.Log.Error(string.Format("GetStatusColor: {0}\n{1}\n{2} ", ex.Message));

        }
    }
    private async Task GetFileLinkType(string search = "", bool overwrite = true, bool prefill = false)
    {
        try
        {

            await Instances.Instance.rest.ExecuteRun<List<LanguageResourceFileDto>>("",
                GetFileCallBack, Constants.GetLanguageResourceFile, HttpMethod.Get);
        }
        catch (Exception ex)
        {
            await App.Current.MainPage.DisplayAlert("Alert", "Something went wrong, check your Internet connectivity or contact Admin", "ok");
            Instances.Instance.Log.Error(string.Format("", ex.Message));
        }

    }
    private async void GetFileCallBack(RestResponse<List<LanguageResourceFileDto>> result)
    {
        try
        {


            if (result != null)
            {
                var t = result.Data.result.Where(x => x.id == 1).FirstOrDefault();
                var tt = t.Texts;

                var json = JsonConvert.SerializeObject(tt);
                var dictionary = JsonConvert.DeserializeObject<List<Text>>(json);
                // foreach (var item in dictionary)
                Instances.Instance.LanguageConvertList = dictionary;

                string ResourceJson = ContentJsonSerializer.JsonSerialize(Instances.Instance.LanguageConvertList);
                Preferences.Set("ResourceJson", ResourceJson);

            }
            // RaisePropertyChanged(() => LanguageResource);
        }
        catch (Exception ex)
        {
            await App.Current.MainPage.DisplayAlert("Alert", "Something went wrong, check your Internet connectivity or contact Admin", "ok");
        }
    }
}
