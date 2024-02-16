var builder = DistributedApplication.CreateBuilder(args);

// ⚠️ Check the port on prometheus/prometheus.yml host.docker.internal:5019
builder.AddContainer("prometheus", "prom/prometheus")
    .WithVolumeMount("../prometheus", "/etc/prometheus")
    .WithEndpoint(containerPort: 9090, hostPort: 9090, scheme: "http");

builder.AddContainer("grafana", "grafana/grafana")
    .WithVolumeMount("../grafana/config", "/etc/grafana")
    .WithVolumeMount("../grafana/dashboards", "/var/lib/grafana/dashboards")
    .WithEndpoint(containerPort: 3000, hostPort: 3000, name: "grafana-http", scheme: "http");

var apiService = builder.AddProject<Projects.GrafanaDashboards_ApiService>("apiservice");

builder.AddProject<Projects.GrafanaDashboards_Web>("webfrontend")
    .WithReference(apiService);

builder.Build().Run();
