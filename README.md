# EW-Design - Diseñador Visual de Páginas Web

## 🎯 Descripción

EW-Design es una aplicación de escritorio WPF que permite crear páginas web de forma visual mediante un sistema de componentes drag-and-drop. El sistema incluye funcionalidades avanzadas de exportación e importación de proyectos.

## ✨ Características Principales

### 🎨 Diseño Visual
- **Interfaz intuitiva** con drag-and-drop
- **Componentes predefinidos** (NavBar, Body, Button, Card, Text, Menu)
- **Editor de propiedades** en tiempo real
- **Vista previa** instantánea de cambios

### 💾 Sistema de Proyectos
- **Exportar proyectos** en formato JSON
- **Importar proyectos** guardados previamente
- **Persistencia completa** de componentes y propiedades
- **Validación de integridad** de archivos

### 🔧 Componentes Disponibles

#### Layout Components
- **NavBar**: Barra de navegación con título y menú personalizable
- **Body**: Contenedor principal con secciones hero y features

#### Design Components
- **Text**: Elementos de texto con propiedades de fuente completas
- **Button**: Botones con estilos personalizables
- **Card**: Tarjetas con título y contenido
- **Menu**: Menús de navegación

### 🚀 Funcionalidades Avanzadas

#### Exportar Proyecto
- Guarda el estado completo del lienzo
- Incluye todos los componentes y propiedades
- Genera archivo JSON con timestamp
- Mantiene el lienzo intacto

#### Importar Proyecto
- Carga proyectos guardados
- Restaura componentes con todas sus propiedades
- Valida integridad del archivo
- Muestra información del proyecto

#### Limpiar Lienzo
- Elimina todos los componentes
- Confirma la acción antes de ejecutar
- Prepara el lienzo para nuevos proyectos

## 🛠️ Tecnologías Utilizadas

- **.NET Framework 4.7.2**
- **WPF (Windows Presentation Foundation)**
- **MVVM Pattern**
- **Newtonsoft.Json** para serialización
- **Xceed.Wpf.Toolkit** para controles avanzados

## 📁 Estructura del Proyecto

```
EWDesign/
├── Components/
│   ├── Models/          # Modelos de componentes
│   └── Views/           # Vistas XAML de componentes
├── Core/
│   ├── Code Generator/  # Generador de código HTML/CSS
│   └── Core Resources/  # Recursos compartidos
├── Model/               # Modelos base y datos de proyecto
├── Services/            # Servicios (ProjectService)
├── View/                # Vistas principales
├── ViewModel/           # ViewModels
├── Documentation/       # Documentación
└── Examples/            # Archivos de ejemplo
```

## 🎮 Cómo Usar

### 1. Crear un Proyecto
1. Abre EW-Design
2. Selecciona "New" en la interfaz principal
3. Arrastra componentes NavBar y Body al lienzo
4. Personaliza propiedades según necesites

### 2. Exportar Proyecto
1. Ve a `File` → `Export Project`
2. Selecciona ubicación para guardar
3. El sistema exportará el proyecto manteniendo el lienzo

### 3. Importar Proyecto
1. Ve a `File` → `Import Project`
2. Selecciona archivo JSON del proyecto
3. El sistema cargará el proyecto completo

### 4. Limpiar Lienzo
1. Ve a `File` → `Clear Canvas`
2. Confirma la acción
3. El lienzo estará listo para nuevos proyectos

## 📋 Requisitos del Sistema

- **Windows 10/11**
- **.NET Framework 4.7.2** o superior
- **2GB RAM** mínimo
- **500MB** espacio en disco

## 🔧 Instalación

1. **Clona el repositorio**:
   ```bash
   git clone [url-del-repositorio]
   ```

2. **Abre la solución** en Visual Studio 2019/2022

3. **Restaura los paquetes NuGet**:
   - Extended.Wpf.Toolkit
   - Newtonsoft.Json

4. **Compila y ejecuta** el proyecto

## 📖 Documentación

- **Guía de Exportar/Importar**: `Documentation/ExportImportGuide.md`
- **Ejemplo de proyecto**: `Examples/ejemplo_proyecto.json`

## 🎨 Ejemplo de Archivo JSON

```json
{
  "projectName": "Mi Proyecto",
  "exportDate": "2024-01-15T10:30:00",
  "version": "1.0",
  "navBar": {
    "type": "NavBar",
    "title": "Mi Producto",
    "backgroundColor": "#f5f7fa",
    "navBarElements": ["Inicio", "Características", "Precios", "Contacto"]
  },
  "body": {
    "type": "Body",
    "backgroundColor": "#1E1E2F",
    "heroSectionComponents": [
      {
        "type": "Text",
        "text": "Bienvenido",
        "foregroundColor": "#ffffff",
        "fontSize": "48"
      }
    ]
  }
}
```

## 🐛 Solución de Problemas

### Error de Exportación
- Verifica que tienes NavBar y Body en el lienzo
- Asegúrate de tener permisos de escritura
- Revisa espacio en disco

### Error de Importación
- Verifica que el archivo JSON no esté corrupto
- Confirma que es un archivo exportado por EW-Design
- Revisa la versión del archivo

### Componentes No Cargados
- Verifica que los tipos sean soportados
- Revisa que las propiedades sean válidas
- Confirma la estructura del archivo

## 🤝 Contribuir

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo `LICENSE` para más detalles.

## 📞 Soporte

Si tienes problemas o preguntas:
- Revisa la documentación en `Documentation/`
- Abre un issue en el repositorio
- Contacta al equipo de desarrollo

---

**EW-Design** - Diseñando el futuro web, un componente a la vez. 🚀
