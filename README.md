# Market Management Application

Market Management est une application de gestion d'un supermarché, permettant la gestion de remises avec plusieurs types de remises possibles. L'application est développée en utilisant l'approche Domain-Driven Design (DDD), où toutes les règles métier sont centralisées dans le domaine.

## 📦 Fonctionnalités

- Gestion des panier.
- Système avancé de gestion des remises (remises sur le produit, remises globales, etc.).

## 🛠️ Architecture

L'application suit les principes du **Domain-Driven Design (DDD)** :

- **Domain** : Contient les entités, les agrégats, les objets de valeur, ainsi que les règles métier.
- **Infrastructure** : Implémente les services d'infrastructure, comme le stockage .

### 📂 Structure

- `Domain` : Contient la logique métier, les agrégats et les règles de domaine.
- `Application` : Gère la logique applicative et les interactions avec les domaines(app console).
- `Infrastructure` : Gère les services techniques tels que les repositories.

### 📖 Repository Pattern

Le **Repository Pattern** est introduit dans cette implémentation DDD pour gérer la persistance des agrégats. L'implémentation du repository est faite **In-Memory** (sans base de données). Notez qu'il ne faut pas le confondre avec le DAO (Data Access Object) ou DAL (Data Access Layer). Les repositories dans DDD ont une responsabilité différente, se concentrant sur la gestion des agrégats uniquement.

### 🧩 Implementation proche du pattern Strategy pour la Gestion des Remises

une approche proche du pattern **Strategy** afin de modifier ou échanger facilement les différentes stratégies de remise. Cela permet de rendre le système de gestion des remises flexible et extensible. Chaque type de remise est implémenté comme une stratégie distincte, facilitant l'ajout ou le changement des comportements sans impact sur le reste de l'application.

## 🚀 CI/CD Pipeline

Une pipeline **GitHub Actions** est mise en place pour automatiser les tâches suivantes :

1. **Tests** : Exécute les tests unitaires pour vérifier le bon fonctionnement des règles métier et de l'application.
2. **Publication** : Génére un exécutable `.exe` et le publie dans une branche `deploy`.

## 🐳 Lancer l'application avec Docker

L'application peut être lancée via Docker. Pour cela, exécutez simplement le script `dockerrun.bat` :

```bash
dockerrun.bat
```
## 😊 Have Fun!

