# Guía de Exportar/Importar Proyectos - EW-Design

## 📋 Descripción General

El sistema de exportar/importar permite guardar y cargar proyectos completos de EW-Design en formato JSON, manteniendo todos los componentes, propiedades y configuraciones.

## 🚀 Funcionalidades

### ✅ Exportar Proyecto
- Guarda el estado completo del lienzo (NavBar + Body)
- Incluye todos los componentes hijos y sus propiedades
- Preserva colores, fuentes, tamaños y posiciones
- Genera archivo JSON con timestamp automático
- Mantiene el lienzo intacto después de exportar

### ✅ Importar Proyecto
- Carga proyectos guardados previamente
- Restaura componentes con todas sus propiedades
- Valida integridad del archivo JSON
- Muestra información del proyecto importado

### ✅ Limpiar Lienzo
- Elimina todos los componentes del lienzo
- Confirma la acción antes de ejecutar
- Prepara el lienzo para nuevos proyectos

## 🎯 Cómo Usar

### Exportar un Proyecto

1. **Preparar el lienzo**: Asegúrate de tener un NavBar y un Body en el lienzo
2. **Acceder al menú**: Ve a `File` → `Export Project`
3. **Seleccionar ubicación**: Elige dónde guardar el archivo JSON
4. **Confirmar**: El sistema exportará el proyecto manteniendo el lienzo intacto

### Importar un Proyecto

1. **Acceder al menú**: Ve a `File` → `Import Project`
2. **Seleccionar archivo**: Busca y selecciona el archivo JSON del proyecto
3. **Confirmar**: El sistema cargará el proyecto y mostrará los componentes

### Limpiar el Lienzo

1. **Acceder al menú**: Ve a `File` → `Clear Canvas`
2. **Confirmar**: Confirma que deseas limpiar el lienzo
3. **Listo**: El lienzo estará limpio para nuevos proyectos

## 📁 Estructura del Archivo JSON

```json
{
  "projectName": "Mi Proyecto",
  "exportDate": "2024-01-15T10:30:00",
  "version": "1.0",
  "navBar": {
    "type": "NavBar",
    "title": "Mi Producto",
    "backgroundColor": "#f5f7fa",
    "navBarElements": ["Inicio", "Características", "Precios", "Contacto"],
    "navBarElementsColor": "#3a3f47"
  },
  "body": {
    "type": "Body",
    "backgroundColor": "#1E1E2F",
    "heroSectionText": ["Texto del hero..."],
    "featureSectionText": ["Texto de características..."],
    "heroSectionComponents": [
      {
        "id": "guid-unico",
        "type": "Text",
        "text": "Texto del componente",
        "backgroundColor": "#ffffff",
        "foregroundColor": "#000000",
        "fontSize": "24",
        "fontWeight": "Bold"
      }
    ],
    "featureSectionComponents": []
  }
}
```

## ⚠️ Validaciones y Errores

### Validaciones de Exportación
- ✅ Verifica que existan NavBar y Body
- ✅ Valida que los componentes no sean null
- ✅ Confirma permisos de escritura en la ubicación

### Validaciones de Importación
- ✅ Verifica que el archivo existe
- ✅ Valida formato JSON correcto
- ✅ Confirma estructura del proyecto
- ✅ Verifica integridad de datos

### Mensajes de Error
- **"Debe tener un NavBar y un Body en el lienzo"**: Agrega estos componentes antes de exportar
- **"El archivo no existe"**: Verifica la ruta del archivo
- **"Error al leer el archivo JSON"**: El archivo está corrupto o mal formateado
- **"El archivo no contiene un proyecto válido"**: Estructura JSON incorrecta

## 🔧 Componentes Soportados

### Layout Components
- **NavBar**: Barra de navegación con título y menú
- **Body**: Contenedor principal con secciones hero y features

### Design Components
- **Text**: Elementos de texto con propiedades de fuente
- **Button**: Botones con texto y estilos personalizables
- **Card**: Tarjetas con título y contenido
- **Menu**: Menús con elementos de navegación

### Propiedades Guardadas
- ✅ Texto y contenido
- ✅ Colores de fondo y texto
- ✅ Tamaños de fuente
- ✅ Peso de fuente
- ✅ Dimensiones (ancho/alto)
- ✅ Posiciones y márgenes
- ✅ Configuraciones específicas por componente

## 🎨 Ejemplo de Uso Completo

1. **Crear proyecto**: Arrastra NavBar y Body al lienzo
2. **Personalizar**: Modifica colores, textos y propiedades
3. **Agregar componentes**: Añade botones, textos, tarjetas al Body
4. **Exportar**: Guarda el proyecto como JSON
5. **Limpiar**: El lienzo se limpia automáticamente
6. **Importar**: Carga el proyecto guardado
7. **Continuar**: Sigue trabajando desde donde lo dejaste

## 📝 Notas Importantes

- Los archivos se guardan con timestamp automático
- El sistema limpia el lienzo después de exportar exitosamente
- Todos los componentes hijos se preservan en la exportación
- La importación reemplaza completamente el contenido del lienzo
- Los archivos JSON son legibles y editables manualmente

## 🛠️ Solución de Problemas

### Error de Exportación
1. Verifica que tienes NavBar y Body en el lienzo
2. Asegúrate de tener permisos de escritura
3. Revisa que el disco tenga espacio suficiente

### Error de Importación
1. Verifica que el archivo JSON no esté corrupto
2. Confirma que es un archivo exportado por EW-Design
3. Revisa que la versión del archivo sea compatible

### Componentes No Cargados
1. Verifica que los tipos de componentes sean soportados
2. Revisa que las propiedades sean válidas
3. Confirma que el archivo tenga la estructura correcta
