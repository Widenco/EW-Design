# Guía de Exportación e Importación de Proyectos

## Descripción General

El sistema de exportación e importación permite guardar y cargar proyectos completos con todos sus componentes y configuraciones. Esta funcionalidad es especialmente útil para:

- Guardar el trabajo en progreso
- Compartir proyectos entre usuarios
- Crear plantillas reutilizables
- Hacer respaldos de proyectos

## Componentes Soportados

### 1. NavBarComponent
- **Propiedades exportadas:**
  - BackgroundColor (color de fondo)
  - Title (título del navbar)
  - NavBarElements (elementos del menú)
  - NavBarElementsColor (color de los elementos)
  - Children (componentes arrastrados al navbar)

### 2. BodyComponent
- **Propiedades exportadas:**
  - BackgroundColor (color de fondo)
  - HeroSectionText (textos de la sección hero)
  - FeatureSectionText (textos de la sección de características)
  - HeroSectionComponents (componentes de la sección hero)
  - FeatureSectionComponents (componentes de la sección de características)
  - Children (componentes arrastrados al body)

### 3. CardComponent ⭐ **MEJORADO**
- **Propiedades exportadas:**
  - BackgroundColor (color de fondo del card)
  - Width (ancho del card)
  - Height (alto del card)
  - Title (componente de texto del título)
  - Body (componente de texto del cuerpo)
  - Children adicionales (componentes arrastrados al card)

#### Configuración Completa del CardComponent

Los `CardComponent` ahora exportan e importan correctamente:

**Title (Título):**
- Text (contenido del texto)
- ForegroundColor (color del texto)
- FontSize (tamaño de fuente)
- FontWeight (peso de fuente)
- TextAlignment (alineación del texto)
- TextWrap (ajuste de texto)
- Margin (márgenes)

**Body (Cuerpo):**
- Text (contenido del texto)
- ForegroundColor (color del texto)
- FontSize (tamaño de fuente)
- FontWeight (peso de fuente)
- TextAlignment (alineación del texto)
- TextWrap (ajuste de texto)
- Margin (márgenes)

### 4. TextComponent
- **Propiedades exportadas:**
  - Text (contenido del texto)
  - ForegroundColor (color del texto)
  - FontSize (tamaño de fuente)
  - FontWeight (peso de fuente)
  - TextAlignment (alineación del texto)
  - TextWrap (ajuste de texto)
  - Margin (márgenes)

### 5. ButtonComponent
- **Propiedades exportadas:**
  - Text (contenido del botón)
  - BackgroundColor (color de fondo)
  - ForegroundColor (color del texto)
  - FontSize (tamaño de fuente)
  - FontWeight (peso de fuente)

### 6. MenuComponent
- **Propiedades exportadas:**
  - ForegroundColor (color del texto)

## Formato de Archivo

Los proyectos se exportan en formato JSON con la siguiente estructura:

```json
{
  "projectName": "Nombre del Proyecto",
  "exportDate": "2024-01-15T14:30:25.1234567",
  "version": "1.0",
  "navBar": {
    // Configuración del NavBar
  },
  "body": {
    // Configuración del Body
  }
}
```

## Mejoras Implementadas

### ✅ **CardComponent - Exportación Completa del Lienzo** ⭐ **ACTUALIZADO**
- **Exporta TODOS los componentes visibles en el Builder View**
- **No importa si son por defecto o modificados** - exporta todo lo que está presente
- **Respeto exacto de lo que está en el lienzo**
- **Funciona con cualquier cantidad de CardComponents (0, 1, 2, 3, etc.)**
- **Importación inteligente: lista vacía = 0 Cards (no crea por defecto)**
- **Eliminación de duplicados: componentes solo en colecciones específicas**
- **Mantiene todos los estilos y propiedades de texto**
- **Soporta componentes adicionales arrastrados al card**

### ✅ **Exportación Completa del Lienzo** ⭐ **ACTUALIZADO**
- **Exporta exactamente lo que está visible en el Builder View**
- **3 CardComponents por defecto** → Exporta **3 Cards**
- **2 CardComponents** (usuario eliminó 1) → Exporta **2 Cards**
- **1 CardComponent** (usuario eliminó 2) → Exporta **1 Card**
- **0 CardComponents** (usuario eliminó todos) → Exporta **0 Cards**
- **CardComponents modificados** → Exporta **todos los modificados**
- **CardComponents por defecto** → Exporta **todos los por defecto**

### ✅ **Importación Inteligente**
- Detecta automáticamente los tipos de componentes hijos
- Restaura configuraciones específicas según el tipo
- Maneja casos donde faltan datos (usa valores por defecto)

### ✅ **Carga Precisa de Componentes** ⭐ **ACTUALIZADO**
- **Carga exactamente los componentes que fueron exportados**
- **Si no hay CardComponents exportados, crea los 3 por defecto**
- **Evita duplicados durante la importación**
- **Respeto exacto de la cantidad de CardComponents exportados**
- **Comportamiento inteligente:**
  - **Nuevo proyecto** → 3 CardComponents por defecto
  - **Importar con 0 Cards** → 3 CardComponents por defecto
  - **Importar con 1 Card** → Solo 1 CardComponent
  - **Importar con 2 Cards** → Solo 2 CardComponents
  - **Importar con 3+ Cards** → Solo los Cards exportados

### ✅ **Validación Robusta**
- Verifica la existencia de archivos antes de importar
- Valida la estructura del JSON
- Maneja errores de deserialización

## Ejemplo de Uso

### Exportar Proyecto
1. En el menú principal, selecciona "File" → "Export Project"
2. Elige la ubicación y nombre del archivo
3. El proyecto se guardará con todas las configuraciones

### Importar Proyecto
1. En el menú principal, selecciona "File" → "Import Project"
2. Selecciona el archivo JSON del proyecto
3. El proyecto se cargará con todas las configuraciones restauradas

## Archivos de Ejemplo

### `Examples/ejemplo_proyecto.json`
Proyecto completo con 3 CardComponents que muestra todas las funcionalidades.

### `Examples/test_single_card.json` ⭐ **NUEVO**
Proyecto de prueba con **exactamente 1 CardComponent** para verificar la carga precisa.

### `Examples/test_two_cards.json` ⭐ **NUEVO**
Proyecto de prueba con **exactamente 2 CardComponents** para verificar la carga precisa.

### `Examples/test_zero_cards.json` ⭐ **NUEVO**
Proyecto de prueba con **0 CardComponents** para verificar que se crean los 3 por defecto.

### `Examples/test_export_default.json` ⭐ **NUEVO**
Proyecto de ejemplo que muestra cómo se exporta un proyecto con **CardComponents por defecto** (exporta 0 Cards).

### `Examples/test_modified_card.json` ⭐ **NUEVO**
Proyecto de ejemplo que muestra cómo se exporta un proyecto con **1 CardComponent modificado** (exporta solo 1 Card).

### `Examples/test_two_cards_modified.json` ⭐ **NUEVO**
Proyecto de ejemplo que muestra cómo se exporta un proyecto con **2 CardComponents modificados** (exporta solo 2 Cards).

### `Examples/test_removed_cards.json` ⭐ **NUEVO**
Proyecto de ejemplo que muestra cómo se exporta un proyecto donde el usuario **eliminó 1 CardComponent por defecto** (queda con 2 Cards por defecto).

### `Examples/test_export_no_default_cards.json` ⭐ **NUEVO**
Proyecto de ejemplo que muestra cómo se exporta un proyecto con **CardComponents por defecto** (exporta 0 Cards en ambas colecciones).

### `Examples/test_import_no_cards.json` ⭐ **NUEVO**
Proyecto de ejemplo que simula un proyecto importado con **0 CardComponents** (todos eran por defecto).

### `Examples/test_partial_cards.json` ⭐ **NUEVO**
Proyecto de ejemplo que muestra cómo se exporta un proyecto con **solo 2 CardComponents** (usuario eliminó 1 por defecto).

### `Examples/test_two_cards_export.json` ⭐ **NUEVO**
Proyecto de ejemplo que muestra cómo debería exportarse un proyecto con **2 CardComponents** (usuario eliminó 1 por defecto).

Estos archivos demuestran que:
- NavBar configurado
- Body con secciones hero y features
- CardComponents con configuraciones completas de Title y Body
- TextComponents y ButtonComponents en las secciones
- **Respeto exacto de la cantidad de componentes exportados**
- **Creación inteligente de componentes por defecto cuando es necesario**
- **Exportación inteligente que detecta componentes por defecto**

## Notas Importantes

1. **Compatibilidad**: Los archivos exportados son compatibles con versiones futuras
2. **Valores por Defecto**: Si una propiedad no está en el archivo, se usan valores por defecto
3. **Componentes Adicionales**: Los componentes arrastrados a Cards se exportan e importan correctamente
4. **Validación**: El sistema valida la integridad del archivo antes de importar
5. **Exportación Completa**: El sistema exporta TODOS los componentes visibles en el lienzo
6. **Respeto del Lienzo**: Exporta exactamente lo que está presente en el Builder View, sin filtrar

## Solución de Problemas

### Error: "El archivo no contiene un proyecto válido"
- Verifica que el archivo JSON tenga la estructura correcta
- Asegúrate de que contenga las secciones `navBar` y `body`

### Error: "Error al leer el archivo JSON"
- Verifica que el archivo no esté corrupto
- Asegúrate de que sea un archivo JSON válido

### CardComponents no se cargan correctamente
- Verifica que el archivo contenga la sección `children` en los CardComponents
- Asegúrate de que los tipos de componentes hijos sean correctos ("Title Text", "Body Text")

## Próximas Mejoras

- [ ] Soporte para más tipos de componentes
- [ ] Exportación de configuraciones de layout
- [ ] Sistema de versionado de proyectos
- [ ] Compresión de archivos grandes
