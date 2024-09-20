var builder = DistributedApplication.CreateBuilder(args);

// ⚠️ Check the port on prometheus/prometheus.yml host.docker.internal:5019
builder.AddContainer("prometheus", "prom/prometheus")
    .WithBindMount("../prometheus", "/etc/prometheus")
    .WithEndpoint(port: 9090, targetPort: 9090, scheme: "http");

builder.AddContainer("grafana", "grafana/grafana")
    .WithBindMount("../grafana/config", "/etc/grafana")
    .WithBindMount("../grafana/dashboards", "/var/lib/grafana/dashboards")
    .WithEndpoint(port: 3000, targetPort: 3000, name: "grafana-http", scheme: "http");

var apiService = builder.AddProject<Projects.GrafanaDashboards_ApiService>("apiservice");

builder.AddProject<Projects.GrafanaDashboards_Web>("webfrontend")
    .WithReference(apiService);

builder.Build().Run();
