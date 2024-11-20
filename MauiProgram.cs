using Microsoft.Maui.LifecycleEvents;
using Plugin.Firebase.Auth;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using SkiaSharp.Views.Maui.Controls.Hosting;
using DMS.CustomControls;
using ZXing.Net.Maui;
using DMS.ViewModel;
using DMS.Views;
using DMS.Services.Interfaces;
using DMS.Services.Implementations;
#if ANDROID
using DMS.Platforms.Android.Renderers;
using ZXing.Net.Maui;

#endif
//using DMS.Platforms.Android.Renderers;
#if IOS
//using Plugin.Firebase.iOS;
#else
using Plugin.Firebase.Android;
#endif
using Plugin.Firebase.Shared;

namespace DMS;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().RegisterFirebaseServices().UseMauiCompatibility().ConfigureMauiHandlers((handlers) =>
        {
            // handlers.AddCompatibilityRenderer(typeof(MyButton), typeof(MyButtonRenderer));
#if ANDROID
            handlers.AddCompatibilityRenderer(typeof(CustomFrame), typeof(MyFrameRenderer));
            handlers.AddHandler(typeof(AppShell), typeof(AppShellRenderer));
#endif
        }).ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            fonts.AddFont("FontAwesome.otf", "FontAwesome");
            fonts.AddFont("FontAwesomeRegular.otf", "FontAwesomeRegular");
            fonts.AddFont("FontAwesomeSolid.otf", "FontAwesomeSolid");
        }).UseMauiMaps().ConfigureAnimations().UseSkiaSharp(true)
        .UseBarcodeReader()
        .RegisterViewModels()
            .RegisterViews()
        .ConfigureMauiHandlers(h=>
        {
            h.AddHandler(typeof(ZXing.Net.Maui.Controls.CameraBarcodeReaderView), typeof(CameraBarcodeReaderViewHandler));
            h.AddHandler(typeof(ZXing.Net.Maui.Controls.CameraView), typeof(CameraViewHandler));
            h.AddHandler(typeof(ZXing.Net.Maui.Controls.BarcodeGeneratorView), typeof(BarcodeGeneratorViewHandler));
        });

        return builder.Build();
    }

    private static MauiAppBuilder RegisterFirebaseServices(this MauiAppBuilder builder)
    {
       
            builder.ConfigureLifecycleEvents(events =>
        {
#if IOS
           
                //events.AddiOS(iOS => iOS.FinishedLaunching((app, launchOptions) => {
                //    CrossFirebase.Initialize(app, launchOptions, CreateCrossFirebaseSettings());
                //    return false;
                //}));

#else
            events.AddAndroid(android => android.OnCreate((activity, state) => CrossFirebase.Initialize(activity, state, CreateCrossFirebaseSettings())));
#endif
       
    });
        builder.Services.AddSingleton(_ => CrossFirebaseAuth.Current);
        return builder;
    }

    private static CrossFirebaseSettings CreateCrossFirebaseSettings()
    {
        return new CrossFirebaseSettings(isAuthEnabled: true, isCloudMessagingEnabled: true);
    }
    public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<IBrandingService, BrandingService>();
        return mauiAppBuilder;
    }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<AddAdvanceChequePageViewModel>();
        mauiAppBuilder.Services.AddTransient<AddDeliveryAddressPageViewModel>();
        mauiAppBuilder.Services.AddSingleton<AdvanceChequePageViewModel>();
        mauiAppBuilder.Services.AddSingleton<BarCodeScannerPageViewModel>();
        mauiAppBuilder.Services.AddTransient<CompositeProdcutsViewModel>();

        mauiAppBuilder.Services.AddTransient<CreateBrandActivityPageViewModel>();
        mauiAppBuilder.Services.AddTransient<CreateCustomerCaseViewModel>();
        mauiAppBuilder.Services.AddTransient<CreateDealerSalesPart1ViewModel>();
        mauiAppBuilder.Services.AddTransient<CreateDealerSalesPart2ViewModel>();
        mauiAppBuilder.Services.AddTransient<CreateOrderDetailsListViewModal>();
        mauiAppBuilder.Services.AddTransient<CreatePaymentSchedulePageViewModel>();
        mauiAppBuilder.Services.AddTransient<CreatePaymentViewModel>();
        mauiAppBuilder.Services.AddTransient<CreateReferralPageViewModel>();
        mauiAppBuilder.Services.AddTransient<CreateSalesReturnPart2ViewModel>();
        mauiAppBuilder.Services.AddTransient<CreateSalesReturnViewModel>();
        mauiAppBuilder.Services.AddTransient<CreateShopVerificationViewModel>();
        mauiAppBuilder.Services.AddTransient<CreateStockVisibilityViewModel>();
        mauiAppBuilder.Services.AddSingleton<CustomerCaseDetailPageViewModel>();
        //mauiAppBuilder.Services.AddSingleton<DashboardPageViewModel>();
        mauiAppBuilder.Services.AddSingleton<DealerSalesDetailViewModel>();
        mauiAppBuilder.Services.AddSingleton<DealerSalesReturnViewModel>();
        mauiAppBuilder.Services.AddSingleton<DispatchDetailViewModel>();
        mauiAppBuilder.Services.AddSingleton<HelpdeskPageViewModel>();
        mauiAppBuilder.Services.AddSingleton<KnowledgeBaseViewModel>();
        mauiAppBuilder.Services.AddSingleton<LoginPageViewModel>();
        mauiAppBuilder.Services.AddTransient<MyOrderListPageViewModel>();
        mauiAppBuilder.Services.AddTransient<OrderDetailsPageViewModel>();
        mauiAppBuilder.Services.AddSingleton<OtpLoginVerifyViewModel>();
        mauiAppBuilder.Services.AddSingleton<OtpRegisterVerifyViewModel>();
        mauiAppBuilder.Services.AddSingleton<OTPVerificationViewModel>();
        mauiAppBuilder.Services.AddSingleton<PaymentEntryViewModel>();
        mauiAppBuilder.Services.AddSingleton<PaymentSchedulingPageViewModel>();
        mauiAppBuilder.Services.AddSingleton<ProductCatelogDetailPageViewModel>();
        mauiAppBuilder.Services.AddSingleton<ProductCatelogListPageViewModel>();
        mauiAppBuilder.Services.AddSingleton<ProfileViewModel>();
        mauiAppBuilder.Services.AddSingleton<RegisterPageViewModel>();
        mauiAppBuilder.Services.AddSingleton<SalesHierarchyPageViewModel>();
        mauiAppBuilder.Services.AddTransient<SalesOrderProductViewModel>();
        mauiAppBuilder.Services.AddSingleton<SalesReturnDetailViewModel>();
        mauiAppBuilder.Services.AddSingleton<SalesReturnListPageViewModel>();
        mauiAppBuilder.Services.AddSingleton<ShellPageViewModel>();
        mauiAppBuilder.Services.AddSingleton<ShopVerificationViewModal>();
        mauiAppBuilder.Services.AddSingleton<SignUpSuccessfullyViewModal>();
        mauiAppBuilder.Services.AddSingleton<SplashScreen2ViewModel>();
        mauiAppBuilder.Services.AddSingleton<VerificationSuccessPageViewModal>();
        mauiAppBuilder.Services.AddSingleton<VerifyInformationPageViewModel>();
        mauiAppBuilder.Services.AddSingleton<ViewBrandActivityPageViewModel>();
        mauiAppBuilder.Services.AddSingleton<ViewCompositeDetailViewModel>();
        mauiAppBuilder.Services.AddSingleton<ViewCustomerCasePageViewModel>();
        mauiAppBuilder.Services.AddSingleton<ViewDeliveryAddressViewModel>();
        mauiAppBuilder.Services.AddSingleton<ViewReferralListViewModel>();
        mauiAppBuilder.Services.AddSingleton<ViewStockListPageViewModel>();
        mauiAppBuilder.Services.AddSingleton<WelcomeVerifyPageViewModel>();
        mauiAppBuilder.Services.AddSingleton<ProductRegistrationViewModel>();
        mauiAppBuilder.Services.AddTransient<CreateProductRegistrationPage>();
        mauiAppBuilder.Services.AddTransient<CreateProductRegistrationPart2>();
        mauiAppBuilder.Services.AddSingleton<ExtendedWarrantyViewPage>();
        mauiAppBuilder.Services.AddTransient<CreateExdendedWarrantyViewPage>();



        return mauiAppBuilder;
    }
    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<AddAdvanceChequePage>();
        mauiAppBuilder.Services.AddTransient<AddDeliveryAddressPage>();
        mauiAppBuilder.Services.AddSingleton<AdvanceCheque>();
        mauiAppBuilder.Services.AddSingleton<BarCodeScannerPage>();
        mauiAppBuilder.Services.AddTransient<CompositeProductListPage>();

        mauiAppBuilder.Services.AddTransient<CreateBrandActivitypage>();
        mauiAppBuilder.Services.AddTransient<CreateCustomerCasePage>();
        mauiAppBuilder.Services.AddTransient<CreateDealerSalesReturnPart1>();
        mauiAppBuilder.Services.AddTransient<CreateDealerSalesReturnPart2>();
        mauiAppBuilder.Services.AddTransient<CreateOrderDetailsListPage>();
        mauiAppBuilder.Services.AddTransient<CreatePaymentSchedulePageView>();
        mauiAppBuilder.Services.AddTransient<CreatePaymentViewPage>();
        mauiAppBuilder.Services.AddTransient<CreateReferralViewPage>();
        mauiAppBuilder.Services.AddTransient<CreateSalesReturnPage>();
        mauiAppBuilder.Services.AddTransient<CreateSalesReturnPart2>();
        mauiAppBuilder.Services.AddTransient<CreateShopVerificationPage>();
        mauiAppBuilder.Services.AddTransient<CreateStockVisibilityPage>();
        mauiAppBuilder.Services.AddTransient<CustomerCaseDetailPage>();
        //mauiAppBuilder.Services.AddSingleton<DashboardPage>();
        mauiAppBuilder.Services.AddSingleton<DealerSalesDetailPage>();
        mauiAppBuilder.Services.AddSingleton<DealerSalesReturnPage>();
        mauiAppBuilder.Services.AddSingleton<DispatchDetail>();
        mauiAppBuilder.Services.AddSingleton<HelpdeskPage>();
        mauiAppBuilder.Services.AddSingleton<KnowledgeBasePage>();
        mauiAppBuilder.Services.AddSingleton<LoginPage>();
        mauiAppBuilder.Services.AddTransient<MyOrderListPageView>();
        mauiAppBuilder.Services.AddTransient<OrderDetailPage>();
        mauiAppBuilder.Services.AddSingleton<OtpLoginVerifyPage>();
        mauiAppBuilder.Services.AddSingleton<OtpLoginVerifyPageNew>();   //added Tarun@Kansoft 08_03_2024 keyboard fix _START
        mauiAppBuilder.Services.AddSingleton<OtpRegisterVerifyPage>();
        mauiAppBuilder.Services.AddSingleton<OtpRegisterVerifyPageNew>();   //added Tarun@Kansoft 08_03_2024 keyboard fix _START
        mauiAppBuilder.Services.AddSingleton<OTPVerificationPage>();
        mauiAppBuilder.Services.AddSingleton<OTPVerificationPageNew>();   //added Tarun@Kansoft 08_03_2024 keyboard fix _START
        mauiAppBuilder.Services.AddSingleton<PaymentEntryViewPage>();
        mauiAppBuilder.Services.AddSingleton<PaymentSchedulingPageView>();
        mauiAppBuilder.Services.AddSingleton<ProductCatelogDetailPage>();
        mauiAppBuilder.Services.AddSingleton<ProductCatelogListPage>();
        mauiAppBuilder.Services.AddSingleton<Profilepage>();
        mauiAppBuilder.Services.AddSingleton<RegisterPage>();
        mauiAppBuilder.Services.AddSingleton<SalesHierarchyPage>();
        mauiAppBuilder.Services.AddTransient<SalesOrderProductPage>();
        mauiAppBuilder.Services.AddSingleton<SalesReturnDetailPage>();
        mauiAppBuilder.Services.AddSingleton<SalesReturnListPage>();
        mauiAppBuilder.Services.AddSingleton<ShopVerificationPage>();
        mauiAppBuilder.Services.AddSingleton<SignupSuccessfullyViewPage>();
        mauiAppBuilder.Services.AddSingleton<SplashScreen2Viewpage>();
        mauiAppBuilder.Services.AddSingleton<VerificationSuccessPage>();
        mauiAppBuilder.Services.AddSingleton<VerifyInformationPage>();
        mauiAppBuilder.Services.AddSingleton<ViewBrandActivityPage>();
        mauiAppBuilder.Services.AddSingleton<ViewCompositeDetailPage>();
        mauiAppBuilder.Services.AddSingleton<ViewCustomerCasePage>();
        mauiAppBuilder.Services.AddSingleton<ViewDeliveryAddressPage>();
        mauiAppBuilder.Services.AddSingleton<ViewReferralListPage>();
        mauiAppBuilder.Services.AddSingleton<ViewStockListPage>();
        mauiAppBuilder.Services.AddSingleton<WelcomeVerificationPage>();
        mauiAppBuilder.Services.AddSingleton<ProductRegistrationPage>();
        mauiAppBuilder.Services.AddTransient<CreateProductRegistrationPage>();
        mauiAppBuilder.Services.AddTransient<CreateProductRegistrationPart2>();
        mauiAppBuilder.Services.AddSingleton<ExtendedWarrantyViewPage>();
        mauiAppBuilder.Services.AddTransient<CreateExdendedWarrantyViewPage>();
        mauiAppBuilder.Services.AddTransient<LedgerViewPage>();

        return mauiAppBuilder;
    }
}