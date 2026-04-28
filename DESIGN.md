---
name: Modern Enterprise High-Density
colors:
  surface: '#0b1326'
  surface-dim: '#0b1326'
  surface-bright: '#31394d'
  surface-container-lowest: '#060e20'
  surface-container-low: '#131b2e'
  surface-container: '#171f33'
  surface-container-high: '#222a3d'
  surface-container-highest: '#2d3449'
  on-surface: '#dae2fd'
  on-surface-variant: '#c2c6d8'
  inverse-surface: '#dae2fd'
  inverse-on-surface: '#283044'
  outline: '#8c90a1'
  outline-variant: '#424656'
  surface-tint: '#b3c5ff'
  primary: '#b3c5ff'
  on-primary: '#002b75'
  primary-container: '#0066ff'
  on-primary-container: '#f8f7ff'
  inverse-primary: '#0054d6'
  secondary: '#bec6e0'
  on-secondary: '#273044'
  secondary-container: '#40495e'
  on-secondary-container: '#afb8d1'
  tertiary: '#b7c8e1'
  on-tertiary: '#213145'
  tertiary-container: '#63738a'
  on-tertiary-container: '#f6f8ff'
  error: '#ffb4ab'
  on-error: '#690005'
  error-container: '#93000a'
  on-error-container: '#ffdad6'
  primary-fixed: '#dae1ff'
  primary-fixed-dim: '#b3c5ff'
  on-primary-fixed: '#001849'
  on-primary-fixed-variant: '#003fa4'
  secondary-fixed: '#dae2fd'
  secondary-fixed-dim: '#bec6e0'
  on-secondary-fixed: '#121b2e'
  on-secondary-fixed-variant: '#3e475c'
  tertiary-fixed: '#d3e4fe'
  tertiary-fixed-dim: '#b7c8e1'
  on-tertiary-fixed: '#0b1c30'
  on-tertiary-fixed-variant: '#38485d'
  background: '#0b1326'
  on-background: '#dae2fd'
  surface-variant: '#2d3449'
typography:
  headline-sm:
    fontFamily: Inter
    fontSize: 18px
    fontWeight: '600'
    lineHeight: 24px
  body-md:
    fontFamily: Inter
    fontSize: 14px
    fontWeight: '400'
    lineHeight: 20px
  label-sm:
    fontFamily: Inter
    fontSize: 12px
    fontWeight: '500'
    lineHeight: 16px
  code-md:
    fontFamily: ui-monospace, SFMono-Regular, Menlo, Monaco, Consolas, monospace
    fontSize: 13px
    fontWeight: '400'
    lineHeight: 20px
rounded:
  sm: 0.125rem
  DEFAULT: 0.25rem
  md: 0.375rem
  lg: 0.5rem
  xl: 0.75rem
  full: 9999px
spacing:
  unit: 4px
  container-padding: 12px
  gutter: 8px
  density-compact: 4px
  density-normal: 8px
---

## Brand & Style

The design system is engineered for power users who require high-information density without cognitive overload. The brand personality is authoritative, precise, and utilitarian, evoking the feeling of a high-performance instrument rather than a consumer app. 

The aesthetic follows a **Modern Enterprise** movement—a blend of minimalism and functionalism. It prioritizes clarity, structural alignment, and speed of interaction. Every pixel serves a purpose, utilizing subtle visual cues to guide the user through complex relational data structures. The UI should feel "invisible," allowing the data and the code to remain the primary focus.

## Colors

The design system utilizes a sophisticated palette centered around **Deep Navy** and **Slate** to minimize eye strain during long sessions of data analysis. The primary mode is dark, though a light mode variant is supported using high-clarity whites and cool grays.

- **Action Blue**: Used exclusively for primary calls-to-action, active states, and focus indicators.
- **Surface Palette**: Tiered slate shades create a sense of depth and hierarchy between the sidebar, editor, and results pane.
- **Syntax Highlighting**: Distinct but harmonized greens and purples are reserved for code elements to ensure instant keyword recognition without breaking the minimalist aesthetic.

## Typography

The typographic system is split between **Inter** for UI elements and a robust **Monospace** stack for code blocks and data tables. 

UI typography is optimized for legibility at small sizes, enabling high-density layouts. We utilize a slightly tighter letter spacing for labels to maximize horizontal space in sidebars. The monospace font is selected for its distinct character recognition (e.g., O vs 0, l vs 1), which is critical for SQL debugging. All code elements should be rendered at a 13px base to balance information density with readability.

## Layout & Spacing

The design system employs a **Fluid Grid** model with a focus on adjustable panes. The layout is divided into three primary zones: the Object Explorer (sidebar), the SQL Editor (main), and the Results Grid (bottom). 

Spacing follows a strict **4px rhythm**. To achieve "High Density," the design system minimizes vertical padding in lists and tables, using 4px or 8px increments. This ensures that more rows of data and more lines of code are visible simultaneously. Flexible split-panes allow users to customize we according to the complexity of their queries.

## Elevation & Depth

Visual hierarchy is established primarily through **Tonal Layers** and **Low-contrast Outlines** rather than heavy shadows. 

- **Level 0 (Background)**: The deepest layer (Deep Navy), used for the application backdrop.
- **Level 1 (Panels)**: Slightly lighter slate used for the sidebar and editor container.
- **Level 2 (Popovers/Tooltips)**: These utilize a subtle 1px border in a lighter slate and a soft, high-diffusion shadow to appear "floated" above the workspace.
- **Active State**: Indicated by a 2px "Action Blue" border-left or bottom, rather than a color shift of the entire surface.

## Shapes

The shape language is **Soft (0.25rem/4px)**. This subtle rounding provides a modern feel while maintaining the structural rigidity required for a professional tool. 

Inputs, buttons, and tab items should all share this consistent 4px corner radius. Highly interactive elements like table cells or tree-view items remain square to ensure seamless alignment and to maximize every pixel of the grid.

## Components

### High-Density Data Tables
Tables are the core of this design system. They must feature:
- 28px row heights for maximum data visibility.
- Sticky headers with a subtle 1px bottom border.
- Monospace font for cell values to ensure alignment of numerical data.
- Hover states using a subtle slate-lightening effect.

### Tree Views (Object Explorer)
- 20px indentation per level.
- Custom icons for tables, views, and stored procedures using the syntax color palette.
- Hover actions (like "Quick Export") that appear only on row hover to reduce visual clutter.

### Tabbed Interfaces
- Square-edged tabs with a bottom-indicator line in "Action Blue" for the active state.
- Close buttons (X) that appear on hover to maintain a clean appearance.
- Distinct styling between "Global Tabs" (top of app) and "Panel Tabs" (within the results pane).

### Input Fields & Buttons
- Inputs use a dark slate background with a 1px border that turns "Action Blue" on focus.
- Primary buttons are solid "Action Blue" with white text.
- Secondary buttons use a ghost style (border only) to remain unobtrusive.
