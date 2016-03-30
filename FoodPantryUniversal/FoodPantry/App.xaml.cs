using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Store;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FoodPantry
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        private LicenseInformation licenseInformation;

        private void initializeLicense()
        {
            // Initialize the license info for use in the app that is uploaded to the Store.
            // uncomment for release
            //   licenseInformation = CurrentApp.LicenseInformation;

            // Initialize the license info for testing.
            // comment the next line for release
            licenseInformation = CurrentAppSimulator.LicenseInformation;

            licenseInformation.LicenseChanged += LicenseInformation_LicenseChanged;
        }

        private void LicenseInformation_LicenseChanged()
        {
            ReloadLicense(); // update the license information
        }

        private void ReloadLicense()
        {
            if (licenseInformation.IsActive)
            {
                if (licenseInformation.IsTrial)
                {
                    // Show the features that are available during trial only.
                }
                else
                {
                    // Show the features that are available only with a full license.
                }
            }
            else
            {
                // A license is inactive only when there' s an error.
            }
        }

        private void DisplayTrialVersionExpirationTime()
        {
            if (licenseInformation.IsActive)
            {
                if (licenseInformation.IsTrial)
                {
                    var longDateFormat = new Windows.Globalization.DateTimeFormatting.DateTimeFormatter("longdate");

                    // Display the expiration date using the DateTimeFormatter. 
                    // For example, longDateFormat.Format(licenseInformation.ExpirationDate)

                    var daysRemaining = (licenseInformation.ExpirationDate - DateTime.Now).Days;

                    // Let the user know the number of days remaining before the feature expires
                }
                else
                {
                    // ...
                }
            }
            else
            {
                // ...
            }
        }
    }
}
