# EV2-VJ
# Jose Garcia
# jose.garcia79@inacapmail.cl

# Proyecto Integrado - Juego de Plataformas 2D

Un juego de plataformas clásico basado en SuperMario hecho en Unity. El objetivo es simple, controlar al personaje, esquivar las trampas, juntar las 7 monedas ocultas en el nivel y al recolectarlas todas finalizas el nivel.

## Decisiones Técnicas (¿Cómo lo hice?)

**Motor y Plataforma:** Desarrollado en **Unity** usando **C#**, configurado en formato **16:9** para que se vea bien en cualquier pantalla y no se deforme.
**Físicas y Movimiento:** Usamos componentes nativos de Unity como "Rigidbody2D" para la gravedad y las físicas de salto, combinado con raycasts para detectar el suelo y evitar que el personaje flote.
**Gestión de Audio:** Separe los sonidos para que cada cosa tenga su gracia, música de fondo en el menú y el nivel, y efectos independientes para cuando saltas, cuando te haces dano, cuando agarras una moneda, cuando ganas y cuando pierdes todas las vidas. Además, los clics de los botones tienen su propio audio con volumen ajustable.
**Interfaz (UI):** Paneles de menú principal, pausa, victoria y derrota controlados mediante un "GameManager" y un contador de monedas en tiempo real con TextMeshPro.

## Riesgos y Cómo los Solucionamos

**El sonido se cortaba al cambiar de escena o reiniciar:**
  * *Problema:* Al hacer clic en un botón que reiniciaba el juego, el audio moría al instante.
  * *Solución:* Cree objetos de audio temporales independientes para los clics y la victoria para que el sonido termine de reproducirse sin problemas.

**El personaje moría de la nada al caminar:**
  * *Problema:* Las cajas de colisión ("colliders") de las zonas de muerte tocaban mal al personaje.
  * *Solución:* Ajuste las capas del escenario ("LayerMask") y achique los colliders para que solo detecten al jugador cuando realmente cae donde no debe.
