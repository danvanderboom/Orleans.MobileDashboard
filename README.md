# Orleans.MobileDashboard
Cross-platform mobile dashboard app for the [Orleans](http://dotnet.github.io/orleans/) virtual actor-based microservices platform.

### Purpose
Allow operations staff and developers to:
- remotely connect to an Orleans cluster from any modern mobile device or Windows computer (iOS, Android, UWP)
- monitor the health and activity of Orleans clusters, silos, and grains in real time
- view configuration and deployment details

Due to the high level of overlap between Orleans and Azure Service Fabric, extending this dashboard to connect to either system would likely provide value to a much larger audience for a small additional investment in development.

### Audience
This dashboard can be used in development and QA environments where it would be used primarily by developers, or it could be deployed into production systems as well where it could be used by infrastructure/operations teams.

### Use Cases
- Add cluster connection - login to cluster management/monitoring service hosted in a cluster, offer to cache credentials
- View cluster details - static configuration data and dynamic (streaming) metrics
- View silo/node details - static configuration data and dynamic (streaming) metrics
- View grain/actor details - ...?
- View cluster map - show all deployed nodes in grid of fault and upgrade domains, color-coded to show status of each
- Receive push notifications if any monitoring triggers are activated - report node and cluster failures, restarts, etc
- View list of notifications, and subscribe or unsubscribe from each one

More features TBD. Open to input from others.

### Alpha Warning
Not only is this project mega-alpha (just a UI layout prototype at this point), but it's also using the Alpha channel for Xamarin tools and Xamarin.Forms libraries. So if you're looking to build the code, you'll need those. This applies to Xamarin Studio on Mac as well as Visual Studio or Xamarin Studio on Windows.