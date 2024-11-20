using Acr.UserDialogs;
using DMS.CustomControls;
using DMS.Dtos;
using DMS.Models;
using DMS.ViewModel;
using DMS.Views;
using Microsoft.Maui.Controls;
using SkiaSharp;
using System.Collections.ObjectModel;

namespace DMS;

public partial class AppShell : Shell
{
    ViewCell lastCell;
	public AppShell()
    {
      
        Shell.SetNavBarIsVisible(this, false);
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
        AppShell.InitializeRouting();
        BindingContext = new ShellPageViewModel();
      

    }

    private void MenuItemsListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        
    }

    private void ViewCell_Tapped(object sender, EventArgs e)
    {
        
        if (lastCell != null)
            lastCell.View.BackgroundColor = Colors.Transparent;
        var viewCell = (ViewCell)sender;
        if (viewCell.View != null)
        {
            viewCell.View.BackgroundColor = Color.FromHex("#EAF4FF");
            lastCell = viewCell;
            //Instances.Instance.ViewCellsMenuList = new ObservableCollection<ViewCell>();
            //Instances.Instance.ViewCellsMenuList.Add(lastCell);
            ((ShellPageViewModel) BindingContext).NavigateAsync(viewCell.ClassId);
            //ViewCell tempCell= lastCell.FindByName<ViewCell>("Home");
            //if (tempCell != null)
            //{
            //    Instances.Instance.tempCell = tempCell;
            //}
        }

    }
    private static void InitializeRouting()
    {
        Routing.RegisterRoute("AddAdvanceChequePage", typeof(AddAdvanceChequePage));
        Routing.RegisterRoute("AddDeliveryAddressPage", typeof(AddDeliveryAddressPage));
        Routing.RegisterRoute("AdvanceCheque", typeof(AdvanceCheque));
        Routing.RegisterRoute("BarCodeScannerPage", typeof(BarCodeScannerPage));
        Routing.RegisterRoute("CompositeProductListPage", typeof(CompositeProductListPage));
        Routing.RegisterRoute("CreateBrandActivitypage", typeof(CreateBrandActivitypage));
        Routing.RegisterRoute("CreateCustomerCasePage", typeof(CreateCustomerCasePage));
        Routing.RegisterRoute("CreateDealerSalesReturnPart1", typeof(CreateDealerSalesReturnPart1));
        Routing.RegisterRoute("CreateDealerSalesReturnPart2", typeof(CreateDealerSalesReturnPart2));
        Routing.RegisterRoute("CreateOrderDetailsListPage", typeof(CreateOrderDetailsListPage));
        Routing.RegisterRoute("CreatePaymentSchedulePageView", typeof(CreatePaymentSchedulePageView));
        Routing.RegisterRoute("CreatePaymentViewPage", typeof(CreatePaymentViewPage));
        Routing.RegisterRoute("CreateReferralViewPage", typeof(CreateReferralViewPage));
        Routing.RegisterRoute("CreateSalesReturnPage", typeof(CreateSalesReturnPage));
        Routing.RegisterRoute("CreateSalesReturnPart2", typeof(CreateSalesReturnPart2));
        Routing.RegisterRoute("CreateShopVerificationPage", typeof(CreateShopVerificationPage));
        Routing.RegisterRoute("CreateStockVisibilityPage", typeof(CreateStockVisibilityPage));
        Routing.RegisterRoute("CustomerCaseDetailPage", typeof(CustomerCaseDetailPage));
        //Routing.RegisterRoute("DashboardPage", typeof(DashboardPage));
        Routing.RegisterRoute("DealerSalesDetailPage", typeof(DealerSalesDetailPage));
        Routing.RegisterRoute("DealerSalesReturnPage", typeof(DealerSalesReturnPage));
        Routing.RegisterRoute("DispatchDetail", typeof(DispatchDetail));
        Routing.RegisterRoute("HelpdeskPage", typeof(HelpdeskPage));
        Routing.RegisterRoute("KnowledgeBasePage", typeof(KnowledgeBasePage));
        Routing.RegisterRoute("LoginPage", typeof(LoginPage));
        Routing.RegisterRoute("MyOrderListPageView", typeof(MyOrderListPageView));
        Routing.RegisterRoute("OrderDetailPage", typeof(OrderDetailPage));
        //Routing.RegisterRoute("OtpLoginVerifyPage", typeof(OtpLoginVerifyPage));
        //Routing.RegisterRoute("OtpRegisterVerifyPage", typeof(OtpRegisterVerifyPage));
        //Routing.RegisterRoute("OTPVerificationPage", typeof(OTPVerificationPage));
        Routing.RegisterRoute("OtpLoginVerifyPage", typeof(OtpRegisterVerifyPageNew));  //added Tarun@Kansoft 08_03_2024 keyboard fix _START
        Routing.RegisterRoute("OtpRegisterVerifyPage", typeof(OtpRegisterVerifyPageNew));  //added Tarun@Kansoft 08_03_2024 keyboard fix _START
        Routing.RegisterRoute("OTPVerificationPage", typeof(OTPVerificationPageNew));  //added Tarun@Kansoft 08_03_2024 keyboard fix _START
        Routing.RegisterRoute("PaymentEntryViewPage", typeof(PaymentEntryViewPage));
        Routing.RegisterRoute("PaymentSchedulingPageView", typeof(PaymentSchedulingPageView));
        Routing.RegisterRoute("ProductCatelogDetailPage", typeof(ProductCatelogDetailPage));
        Routing.RegisterRoute("ProductCatelogListPage", typeof(ProductCatelogListPage));
        Routing.RegisterRoute("Profilepage", typeof(Profilepage));
        Routing.RegisterRoute("RegisterPage", typeof(RegisterPage));
        Routing.RegisterRoute("SalesHierarchyPage", typeof(SalesHierarchyPage));
        Routing.RegisterRoute("SalesOrderProductPage", typeof(SalesOrderProductPage));
        Routing.RegisterRoute("SalesReturnDetailPage", typeof(SalesReturnDetailPage));
        Routing.RegisterRoute("SalesReturnListPage", typeof(SalesReturnListPage));
        Routing.RegisterRoute("ShopVerificationPage", typeof(ShopVerificationPage));
        Routing.RegisterRoute("SignupSuccessfullyViewPage", typeof(SignupSuccessfullyViewPage));
        Routing.RegisterRoute("SplashScreen2Viewpage", typeof(SplashScreen2Viewpage));
        Routing.RegisterRoute("VerificationSuccessPage", typeof(VerificationSuccessPage));
        Routing.RegisterRoute("VerifyInformationPage", typeof(VerifyInformationPage));
        Routing.RegisterRoute("ViewBrandActivityPage", typeof(ViewBrandActivityPage));
        Routing.RegisterRoute("ViewCompositeDetailPage", typeof(ViewCompositeDetailPage));
        Routing.RegisterRoute("ViewCustomerCasePage", typeof(ViewCustomerCasePage));
        Routing.RegisterRoute("ViewDeliveryAddressPage", typeof(ViewDeliveryAddressPage));
        Routing.RegisterRoute("ViewReferralListPage", typeof(ViewReferralListPage));
        Routing.RegisterRoute("ViewStockListPage", typeof(ViewStockListPage));
        Routing.RegisterRoute("WelcomeVerificationPage", typeof(WelcomeVerificationPage));
        Routing.RegisterRoute("ProductRegistrationPage", typeof(ProductRegistrationPage));
        Routing.RegisterRoute("CreateProductRegistrationPage", typeof(CreateProductRegistrationPage));
        Routing.RegisterRoute("CreateProductRegistrationPart2", typeof(CreateProductRegistrationPart2));
        Routing.RegisterRoute("ExtendedWarrantyViewPage", typeof(ExtendedWarrantyViewPage));
        Routing.RegisterRoute("CreateExdendedWarrantyViewPage", typeof(CreateExdendedWarrantyViewPage));
        Routing.RegisterRoute("LedgerViewPage", typeof(LedgerViewPage));
        Routing.RegisterRoute("EarnedSchemes", typeof(EarnedSchemes)); //added Tarun@kansoft 22-05-2024 Earned Schemes List ---------- ( #SR-3480 )
        Routing.RegisterRoute("EarnedSchemesDetail", typeof(EarnedSchemesDetail)); //added Tarun@kansoft 22-05-2024 Earned Schemes List ---------- ( #SR-3480 )
    }
}
