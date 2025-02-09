# Spawn System and Services

## Description
This part of the project implements a robust and flexible entity spawn system, leveraging design patterns like Object Pooling, Factory, and Service Locator to enhance performance and scalability. These patterns ensure loose coupling between components, making the code easier to maintain, extend, and test.

---
## Key Components

### 1. EntitySpawnFactory (Factory Pattern)
The `EntitySpawnFactory` class centralizes the creation and spawning of entities. It enables easy management of different object types, such as players and other in-game entities. Inside this class, various spawn strategies are implemented, including chessboard-style placement and stair-like arrangements, allowing for flexible positioning of spawned entities.

#### Advantages:
- Separation of entity creation logic enhances readability and maintainability.
- Adding new entity types is simplified without changing the factory logic.

### 2. CustomPool (Object Pooling Pattern)
Object pooling is implemented using the `CustomPool` class. This pattern significantly boosts performance by reusing objects instead of constantly creating and destroying them, reducing the burden on memory and garbage collection.

#### Advantages:
- Prevents frequent memory allocations and garbage collection.
- Scales automatically by creating new objects only when necessary.

### 3. IAssetProvider (Dependency Injection Pattern)
The `AssetProvider` class encapsulates the logic for loading and instantiating prefabs, centralizing asset management. This approach reduces dependencies in the core gameplay code and enhances testability.

#### Advantages:
- Asset management is decoupled from the game logic.
- Enhances flexibility and testability.

### 4. Service System (Service Locator Pattern)
The `AllServices` class implements the Service Locator pattern, allowing centralized access to game services. This makes it easy to retrieve any registered service without tightly coupling the game logic to specific service implementations.

#### Advantages:
- Easy to add new services without altering existing functionality.
- Centralized service state management reduces component coupling.

---
## Approaches and Patterns

### Encapsulation of Entity Spawning and Object Lifecycle Management
- Using the Factory pattern isolates entity creation logic from core gameplay mechanics.
- Object pooling improves performance by managing object lifecycles and reusing objects.

### Efficient Object Management with Pooling
- Object Pooling optimizes performance, especially in games with numerous objects of the same type.
- The pool ensures objects are reused efficiently, avoiding instantiation and destruction overhead.

### Centralized Asset and Service Management
- Dependency Injection and Service Locator patterns enable clean and flexible management of assets and services, making the system easier to test and modify.

### Extensibility and Maintainability
- Each component is designed to be modular and extensible.
- New features can be added without modifying existing code, ensuring adaptability to future requirements.

---
## Usage Examples

### Registering a Service
```csharp
AllServices.Container.RegisterSingle<IMyService>(new MyService());
```

### Getting a Service
```csharp
var myService = AllServices.Container.Single<IMyService>();
```

### Spawning Entities
```csharp
var entity = entitySpawnFactory.SpawnDefaultEntity();
```

### Using Object Pools
```csharp
var entity = defaultEntityPool.Get();
```

---
# UI System for Game Project

## Overview
This project implements a modular and scalable UI system in Unity, leveraging an event-driven architecture and Dependency Injection (DI) for managing UI windows and interactions.

## Key Features
- **Window Registration & Management:** Windows are dynamically registered and managed through the `WindowController`.
- **Layered UI:** Organized into layers (`HUD`, `Popup`, `Overlay`, `FullScreen`) for better performance.
- **Modular Window System:** Each window is a `UIElement` that can be customized and controlled individually.
- **Dependency Injection Support:** The system utilizes DI for handling service access and dependencies.

## Components

### 1. WindowRegistry
- Holds references to registered window prefabs and manages updates dynamically.

### 2. WindowController
- Manages the lifecycle of UI windows, ensuring only one instance of a window is open at a time.

### 3. UIRoot
- Manages different UI layers (`HUD`, `Popup`, `Overlay`, `FullScreen`).

### 4. UIElement
- Base class for all UI windows, handling animations and visibility toggling.

### 5. GameView
- An example UI window that interacts with the `WindowController`.

## Usage

### Setting Up WindowRegistry
1. Create a `WindowRegistry` instance in the Unity Editor.
2. Add window prefabs to the `windowPrefabs` list.

### Registering Windows
```csharp
WindowController.OpenWindow<SettingsWindowView>();
```

### Managing UI Layers
```csharp
UIRoot.GetLayer(UILayer.Popup);
```

### Opening and Closing Windows
```csharp
WindowController.OpenWindow<T>();
WindowController.CloseWindow<T>();
```

## Future Improvements
- Custom animations for window transitions.
- Extended window types like tooltips and modals.

---
# Camera System

## Overview
This project includes a camera control system for a 3D game, supporting horizontal movement via dragging and player following. The system uses the Strategy Pattern to switch between movement methods dynamically.

## Key Components

### 1. CameraObjectsData
- Stores references to essential camera objects (`camera`, `holder`, `rotation`, `tracking plane`).

### 2. HorizontalCameraMovementStrategy
- Manages switching between dragging and following modes.

### 3. FollowPlayerBehaviour
- Handles smooth camera following.

### 4. MoveByDragBehaviour
- Implements camera movement via dragging, considering inertia and map boundaries.

### 5. MapLimitsForCameraData
- Defines camera movement limits and smooth return parameters.

## Usage Example
```csharp
var moveByDragBehaviour = new MoveByDragBehaviour(cameraObjectsData, dragMovementData, mapLimitsForCameraData, this);
moveByDragBehaviour.ProcessStartDrag(screenPosition);
moveByDragBehaviour.ProcessDrag(screenPosition);
moveByDragBehaviour.ProcessEndDrag();
```

---
# Player Control System

## Overview
This system manages the player's movement and interaction using modular services that provide flexibility and scalability. It includes event-based input handling and physics-driven movement.

## Key Components

### 1. Player Component
- Handles movement and jump actions.
- Uses `InputService` for event-driven input handling.
- Relies on `MovementService` for physics-based movement.

### 2. MovementService
- Implements `IMovementService` to control movement.
- Uses `Rigidbody.velocity` for smooth motion.
- Ensures realistic jumping mechanics with `Physics.Raycast` for ground detection.

### 3. InputService
- Implements `IInputService` for handling user input.
- Uses event-driven architecture with `OnMove` and `OnJump` events.
- Manages input lifecycle and allows for flexible input remapping.
