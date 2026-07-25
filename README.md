# 🚀 Cloud-Native DevOps Project on AWS

> Production-ready end-to-end DevOps pipeline for deploying an ASP.NET Core E-Commerce API on Kubernetes using AWS, Docker, GitHub Actions, Trivy, NGINX Ingress, and AWS Application Load Balancer.

---

# 📌 Project Overview

This project demonstrates the implementation of a complete Cloud-Native DevOps workflow for deploying an ASP.NET Core E-Commerce application into a Kubernetes cluster running on AWS.

The project focuses on designing and automating the complete software delivery lifecycle including:

- Docker Containerization
- CI/CD Automation
- Kubernetes Deployment
- Cloud Infrastructure
- Persistent Storage
- Ingress Management
- Load Balancing
- Security Scanning

> **Note**
>
> The ASP.NET Core E-Commerce API used in this project was developed by a teammate (Application Developer).
>
> My responsibility was implementing the complete DevOps infrastructure and deployment pipeline around the application, including containerization, CI/CD, Kubernetes, AWS integration, storage, ingress, and production deployment.

---

# 🏗 Architecture

                                          ┌───────────────────────────┐
                                          │        Developer          │
                                          │      (GitHub Push)        │
                                          └─────────────┬─────────────┘
                                                        │
                                                        ▼
                                         ┌─────────────────────────────┐
                                         │       GitHub Repository     │
                                         │  ecommerce-devops-project   │
                                         └─────────────┬───────────────┘
                                                       │
                                             GitHub Actions CI/CD
                                                       │
                 ┌─────────────────────────────────────┴─────────────────────────────────────┐
                 │                                                                           │
                 ▼                                                                           ▼
      ┌─────────────────────┐                                                ┌──────────────────────────┐
      │      Build Job      │                                                │      Deploy Job         │
      │                     │                                                │                          │
      │ • Docker Build      │                                                │ kubectl apply -f k8s/   │
      │ • Docker Push       │────────────── Docker Hub ─────────────────────▶│ using kubeconfig Secret │
      └─────────────────────┘                                                └─────────────┬────────────┘
                                                                                           │
                                                                                           ▼
================================================================================ AWS =================================================================================

                                             Kubernetes Cluster (kubeadm)

      ┌─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────┐
      │                                                                                                                     │
      │      Control Plane EC2                                                                       Worker EC2 #1          │
      │ ┌────────────────────────┐                                                      ┌────────────────────────────────┐  │
      │ │ API Server             │                                                      │ ecommerce Deployment           │  │
      │ │ Scheduler              │                                                      │                                │  │
      │ │ Controller Manager     │                                                      │  ┌────────────────────────┐    │  │
      │ │ etcd                   │                                                      │  │ Ecommerce API Pod      │    │  │
      │ │ kubectl                │                                                      │  │ ASP.NET Core (.NET 9)  │    │  │
      │ └────────────────────────┘                                                      │  └────────────┬───────────┘    │  │
      │                                                                                 │               │                │  │
      │                                                                                 │               ▼                │  │
      │                                                                                 │      PVC → Amazon EBS          │  │
      │                                                                                 └────────────────────────────────┘  │
      │                                                                                                                     │
      │                                                                                 Worker EC2 #2                       │
      │                                                      ┌──────────────────────────────────────────────────────────┐   │
      │                                                      │ SQL Server Deployment                                    │   │
      │                                                      │                                                          │   │
      │                                                      │  ┌────────────────────────────┐                          │   │
      │                                                      │  │ SQL Server 2022 Pod        │                          │   │
      │                                                      │  └──────────────┬─────────────┘                          │   │
      │                                                      │                 │                                        │   │
      │                                                      │                 ▼                                        │   │
      │                                                      │         PVC → Amazon EBS                                 │   │
      │                                                      └──────────────────────────────────────────────────────────┘   │
      │                                                                                                                     │
      │──────────────────────────────────────────── Kubernetes Networking ───────────────────────────────────────────────── │
      │                                                                                                                     │
      │      ClusterIP Service (sql-svc)             ClusterIP Service (ecommerce-svc)                                      │
      │                     ▲                                      ▲                                                        │
      │                     │                                      │                                                        │
      │                     └─────────────── API ↔ SQL Communication ─────────────────────────────────────────────────────┐ │
      │                                                                                                                   │ │
      │                                    NGINX Ingress Controller                                                       │ │
      │                                             ▲                                                                     │ │
      └─────────────────────────────────────────────┼──────────────────────────────────────────────────────────────────── ┘ │
                                                    │                                                                       │
                                                    ▼                                                                       │
                                       AWS Application Load Balancer (ALB)                                                  │
                                                    │                                                                       │
                                                    ▼                                                                       │
                                              Internet / Browser                                                            │
---

# ⚙ Technology Stack

| Category | Technology |
|------------|----------------|
| Cloud | AWS EC2 |
| Containerization | Docker |
| Container Registry | Docker Hub |
| CI/CD | GitHub Actions |
| Security | Trivy |
| Orchestration | Kubernetes (kubeadm) |
| Ingress | NGINX Ingress Controller |
| Load Balancer | AWS Application Load Balancer (ALB) |
| Storage | AWS EBS CSI Driver |
| Database | Microsoft SQL Server |
| Application | ASP.NET Core Web API |

---

# 📂 Repository Structure

```text
.
├── .github
│   └── workflows
│       └── ci-cd.yml
│
├── src
│   └── ASP.NET Core API
│
├── k8s
│   ├── 00-namespace.yml
│   ├── 01-secret.yml
│   ├── 02-storageclass.yml
│   ├── 03-sql-pvc.yml
│   ├── 04-sql-deployment.yml
│   ├── 05-sql-service.yml
│   ├── 06-api-pvc.yml
│   ├── 07-api-deployment.yml
│   ├── 08-api-service.yml
│   └── 09-ingress.yml
│
├── docker-compose.yml
├── README.md
└── docs
    └── architecture.png
```

---

# 🚀 CI/CD Pipeline

Every push to the **main** branch automatically triggers the following pipeline:

```text
Developer
      │
      ▼
GitHub Repository
      │
      ▼
GitHub Actions
      │
      ├──────────────► Build Docker Image
      │
      ├──────────────► Trivy Image Scan
      │
      ├──────────────► Push Image to Docker Hub
      │
      └──────────────► Deploy to Kubernetes
                              │
                              ▼
                     kubeadm Kubernetes Cluster
                              │
                ┌─────────────┴─────────────┐
                ▼                           ▼
        SQL Server Deployment        API Deployment
                │                           │
                └─────────────┬─────────────┘
                              ▼
                      ClusterIP Services
                              ▼
                 NGINX Ingress Controller
                              ▼
             AWS Application Load Balancer
                              ▼
                          Internet
```

---

# ☁ Infrastructure

- Kubernetes Cluster created using **kubeadm**
- Control Plane hosted on AWS EC2
- Worker Nodes hosted on AWS EC2
- NGINX Ingress Controller
- AWS Application Load Balancer
- AWS EBS CSI Driver
- Persistent Volumes
- Persistent Volume Claims
- Kubernetes Secrets
- Kubernetes Deployments
- Kubernetes Services

---

# 🔐 Security

- Trivy vulnerability scanning
- Kubernetes Secrets
- Private Docker Hub authentication
- SQL Server credentials stored securely
- Separation between infrastructure and application configuration

---

# 📦 Kubernetes Resources

- Namespace
- Secret
- StorageClass
- PersistentVolumeClaim
- SQL Deployment
- SQL Service
- API Deployment
- API Service
- Ingress

---

# 💾 Persistent Storage

## SQL Server

- AWS EBS Volume
- Persistent Volume Claim
- Persistent Database Storage

## Application

- Persistent volume for uploaded images

---

# 🌐 Networking

Internet

↓

AWS Application Load Balancer

↓

NGINX Ingress Controller

↓

ClusterIP Service

↓

ASP.NET Core API

↓

SQL Server

---

# 📈 Features

✅ Dockerized Application

✅ Automated CI/CD Pipeline

✅ Automatic Docker Image Build

✅ Trivy Vulnerability Scanning

✅ Docker Hub Integration

✅ Kubernetes Deployment

✅ SQL Server on Kubernetes

✅ Persistent Storage using AWS EBS

✅ Kubernetes Secrets

✅ NGINX Ingress

✅ AWS Application Load Balancer

✅ Rolling Updates

✅ Infrastructure as Code using Kubernetes Manifests

---

# 📸 Screenshots

Include screenshots for:

- Architecture Diagram
- GitHub Actions Pipeline
- Docker Hub Repository
- Trivy Scan
- Kubernetes Pods
- Kubernetes Services
- Kubernetes Ingress
- AWS ALB
- AWS Target Group
- Running Application

---

# 👨‍💻 My Responsibilities

As the DevOps Engineer on this project, I was responsible for:

- Designing the deployment architecture
- Containerizing the application using Docker
- Creating Docker Compose for local development
- Building the GitHub Actions CI/CD pipeline
- Integrating Trivy security scanning
- Publishing Docker images to Docker Hub
- Writing Kubernetes manifests
- Deploying Kubernetes using kubeadm
- Configuring AWS EBS CSI Driver
- Provisioning persistent storage
- Configuring SQL Server deployment
- Managing Kubernetes Secrets
- Configuring NGINX Ingress Controller
- Integrating AWS Application Load Balancer
- Troubleshooting Kubernetes networking, storage, ingress, and deployment issues
- Automating the deployment process from source code to production

---

# 📚 Skills Demonstrated

- Docker
- Kubernetes
- AWS
- GitHub Actions
- CI/CD
- Trivy
- Docker Hub
- kubeadm
- NGINX Ingress
- AWS ALB
- AWS EBS CSI Driver
- Kubernetes Storage
- Kubernetes Networking
- Infrastructure as Code
- DevOps Automation

---

# 🚀 Future Improvements

- Helm Charts
- ArgoCD GitOps Deployment
- Prometheus Monitoring
- Grafana Dashboards
- Loki Logging
- Horizontal Pod Autoscaler
- TLS with cert-manager
- AWS Route53 Integration
- AWS ExternalDNS
- Multi-Environment Deployment (Dev / Stage / Production)

---

# ⭐ If you found this project useful, don't forget to give it a Star!
