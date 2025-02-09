[🇬🇧 English](README.md) | [🇷🇺 Русский](README.ru.md)

# Spawn System and Services

## Description  
This part of the project implements a reliable and flexible entity spawn system using design patterns such as **Object Pooling**, **Factory**, and **Service Locator**.

---
## Main Components

### 1. EntitySpawnFactory (Factory Pattern)  
The `EntitySpawnFactory` class centralizes the process of creating and spawning entities. It simplifies the management of different object types.

#### Advantages:
- Separating the entity creation logic improves readability and maintainability.
- Adding new entity types does not require modifying the factory code.

### 2. CustomPool (Object Pool Pattern)  
`CustomPool` implements an object pool that significantly improves performance by reusing objects instead of constantly creating and destroying them. This reduces memory load and garbage collection overhead.

#### Advantages:
- Avoids frequent memory allocations and garbage collector activity.
- Automatically scales by creating new objects only when necessary.

### 3. IAssetProvider (Dependency Injection Pattern)  
The `AssetProvider` class encapsulates the logic for loading and creating prefabs, centralizing resource management.

#### Advantages:
- Resource management is separated from game logic.
- Simplifies testing and scalability.

### 4. Service System (Service Locator Pattern)  
The `AllServices` class implements the **Service Locator** pattern, providing centralized access to game services. This makes it easy to retrieve registered services without tightly coupling game logic to specific implementations. If necessary, the implementation can be refactored to use Zenject.

#### Advantages:
- New services can be added without modifying existing code.
- Centralized service state management reduces component coupling.

---
## Usage Examples

### Registering a Service  
```csharp
AllServices.Container.RegisterSingle<IMyService>(new MyService());
```

### Retrieving a Service  
```csharp
var myService = AllServices.Container.Single<IMyService>();
```

### Spawning Entities  
```csharp
var entity = entitySpawnFactory.SpawnDefaultEntity();
```

### Using the Object Pool  
```csharp
var entity = defaultEntityPool.Get();
```

---
# UI System

## Overview  
This project implements a **modular and scalable UI system in Unity**, using **event-driven architecture** and **dependency injection (DI)** for window management and interactions.

## Key Features  
- **Window registration and management:** Windows are dynamically registered and managed via `WindowController`.
- **Layered UI:** Interface is divided into layers (`HUD`, `Popup`, `Overlay`, `FullScreen`) for better performance.
- **Modular window system:** Each window is a `UIElement` that can be customized and managed individually.
- **DI Support:** The system uses **dependency injection** for service and dependency management.

## Components

### 1. WindowRegistry  
- Stores references to registered window prefabs and dynamically manages their updates.

### 2. WindowController  
- Manages the lifecycle of UI windows.

### 3. UIRoot  
- Manages different UI layers (`HUD`, `Popup`, `Overlay`, `FullScreen`).

### 4. UIElement  
- Base class for all UI windows, handling animations and visibility toggling.

### 5. GameView  
- Example of a UI window interacting with `WindowController`.

## Usage

### Setting Up the WindowRegistry  
1. Create a `WindowRegistry` in Unity.  
2. Add window prefabs to the `windowPrefabs` list via the button inside the ScriptableObject.

### Managing UI Layers  
```csharp
UIRoot.GetLayer(UILayer.Popup);
```

### Opening and Closing Windows  
```csharp
WindowController.OpenWindow<T>();
WindowController.CloseWindow<T>();
```
![Open/Close window](https://raw.githubusercontent.com/nerfmee/Spawn/main/Assets/ContentForReadMe/OpenWindow.gif)

---
# Camera System

## Overview  
This project includes a camera control system for a 3D game, supporting **horizontal movement** via **dragging** and **following the player**. The system uses the **Strategy Pattern** to dynamically switch between movement methods.

---
## Main Components

### 1. CameraObjectsData  
- Stores references to key camera objects (`camera`, `holder`, `rotation`, `tracking plane`).

### 2. HorizontalCameraMovementStrategy  
- Manages switching between **dragging** and **following the player** modes.

### 3. FollowPlayerBehaviour  
- Handles **smooth camera following** of the player.

### 4. MoveByDragBehaviour  
- Implements **drag-based camera movement**, considering inertia and map boundaries.

### 5. MapLimitsForCameraData  
- Defines **camera movement boundaries** and **smooth return parameters**.

![Camera Limits](https://raw.githubusercontent.com/nerfmee/Spawn/main/Assets/ContentForReadMe/CameraLimits.gif)

## Example Usage  
```csharp
var moveByDragBehaviour = new MoveByDragBehaviour(cameraObjectsData, dragMovementData, mapLimitsForCameraData, this);
moveByDragBehaviour.ProcessStartDrag(screenPosition);
moveByDragBehaviour.ProcessDrag(screenPosition);
moveByDragBehaviour.ProcessEndDrag();
```

---
# Player Control System

## Overview  
This system handles **player movement** and **environment interactions** using **modular services**. It includes **event-driven input handling** and **physics-based movement**.

## Main Components

### 1. Player Component  
- Handles **player movement** and **jumping**.
- Uses `InputService` for **event-driven input processing**.
- Relies on `MovementService` for **physics-based movement management**.

### 2. MovementService  
- Implements `IMovementService` for movement control.
- Uses `Rigidbody.velocity` for **smooth movement**.
- Ensures **realistic jumping** using `Physics.Raycast` to **detect the ground**.

### 3. InputService  
- Implements `IInputService` for **handling user input**.
- Uses an **event-driven architecture** with `OnMove` and `OnJump` events.
- Manages input lifecycle and supports **control remapping**.

![Player Move](https://raw.githubusercontent.com/nerfmee/Spawn/main/Assets/ContentForReadMe/PlayerMove.gif)
