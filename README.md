# Orleans.MobileDashboard
Cross-platform mobile dashboard app for Orleans actor-based microservices platform.

### Purpose
Allow operations staff and developers to:
- remotely connect to an Orleans cluster from any modern mobile device or Windows computer (iOS, Android, UWP)
- monitor the health and activity of Orleans clusters, silos, and grains in real time
- view configuration and deployment details

Due to the high level of overlap between Orleans and Azure Service Fabric, extending this dashboard to connect to either system would likely provide value to a much larger audience for a small additional investment in development.

### Audience
This dashboard can be used in development and QA environments where it would be used primarily by development and infrastructure teams, or it could be deployed into production systems as well.

### Basic Use Cases
- Add cluster connection - login to cluster management/monitoring service hosted in a cluster, offer to cache credentials
- View cluster details - static configuration data and dynamic (streaming) metrics
- View silo/node details - static configuration data and dynamic (streaming) metrics
- View grain/actor details - ...?
- View cluster map - show all deployed nodes in grid of fault and upgrade domains, color-coded to show status of each
- Receive push notifications if any monitoring triggers are activated - report node and cluster failures, restarts, etc
- View list of notifications, and subscribe or unsubscribe from each one

More features TBD. Open to input from others.

