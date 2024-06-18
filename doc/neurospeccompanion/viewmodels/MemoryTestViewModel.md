# MemoryTestViewModel `Internal class`

## Diagram
```mermaid
  flowchart LR
  classDef interfaceStyle stroke-dasharray: 5 5;
  classDef abstractStyle stroke-width:4px
  subgraph NeuroSpecCompanion.ViewModels
  NeuroSpecCompanion.ViewModels.MemoryTestViewModel[[MemoryTestViewModel]]
  end
  subgraph System.ComponentModel
System.ComponentModel.INotifyPropertyChanged[[INotifyPropertyChanged]]
  end
System.ComponentModel.INotifyPropertyChanged --> NeuroSpecCompanion.ViewModels.MemoryTestViewModel
```

## Details
### Inheritance
 - `INotifyPropertyChanged`

### Constructors
#### MemoryTestViewModel
[*Source code*](https://github.com///blob//NeuroSpecCompanion/ViewModels/MemoryTestViewModel.cs#L15)
```csharp
public MemoryTestViewModel()
```

### Events
#### PropertyChanged
```csharp
public event PropertyChangedEventHandler PropertyChanged
```

*Generated with* [*ModularDoc*](https://github.com/hailstorm75/ModularDoc)
