!/**
 * Highcharts JS v11.4.6 (2024-07-08)
 *
 * Series on point module
 *
 * (c) 2010-2024 Highsoft AS
 * Author: Rafal Sebestjanski and Piotr Madej
 *
 * License: www.highcharts.com/license
 */function(t){"object"==typeof module&&module.exports?(t.default=t,module.exports=t):"function"==typeof define&&define.amd?define("highcharts/modules/series-on-point",["highcharts"],function(e){return t(e),t.Highcharts=e,t}):t("undefined"!=typeof Highcharts?Highcharts:void 0)}(function(t){"use strict";var e=t?t._modules:{};function o(e,o,i,s){e.hasOwnProperty(o)||(e[o]=s.apply(null,i),"function"==typeof CustomEvent&&t.win.dispatchEvent(new CustomEvent("HighchartsModuleLoaded",{detail:{path:o,module:e[o]}})))}o(e,"Series/SeriesOnPointComposition.js",[e["Core/Globals.js"],e["Core/Series/Point.js"],e["Core/Series/Series.js"],e["Core/Series/SeriesRegistry.js"],e["Core/Renderer/SVG/SVGRenderer.js"],e["Core/Utilities.js"]],function(t,e,o,i,s,r){var n,a,p,h=t.composed,d=i.seriesTypes.bubble,c=r.addEvent,f=r.defined,u=r.find,l=r.isNumber,y=r.pushUnique;return(n=p||(p={})).compose=function(t,e){if(y(h,"SeriesOnPoint")){var o=a.prototype,i=o.chartGetZData,s=o.seriesAfterInit,r=o.seriesAfterRender,n=o.seriesGetCenter,p=o.seriesShowOrHide,d=o.seriesTranslate;t.types.pie.prototype.onPointSupported=!0,c(t,"afterInit",s),c(t,"afterRender",r),c(t,"afterGetCenter",n),c(t,"hide",p),c(t,"show",p),c(t,"translate",d),c(e,"beforeRender",i),c(e,"beforeRedraw",i)}return t},a=function(){function t(t){this.getRadii=d.prototype.getRadii,this.getRadius=d.prototype.getRadius,this.getPxExtremes=d.prototype.getPxExtremes,this.getZExtremes=d.prototype.getZExtremes,this.chart=t.chart,this.series=t,this.options=t.options.onPoint}return t.prototype.drawConnector=function(){this.connector||(this.connector=this.series.chart.renderer.path().addClass("highcharts-connector-seriesonpoint").attr({zIndex:-1}).add(this.series.markerGroup));var t=this.getConnectorAttributes();t&&this.connector.animate(t)},t.prototype.getConnectorAttributes=function(){var t=this.series.chart,o=this.options;if(o){var i=o.connectorOptions||{},r=o.position,n=t.get(o.id);if(n instanceof e&&r&&f(n.plotX)&&f(n.plotY)){var a=f(r.x)?r.x:n.plotX,p=f(r.y)?r.y:n.plotY,h=a+(r.offsetX||0),d=p+(r.offsetY||0),c=i.width||1,u=i.stroke||this.series.color,l=i.dashstyle,y={d:s.prototype.crispLine([["M",a,p],["L",h,d]],c),"stroke-width":c};return t.styledMode||(y.stroke=u,y.dashstyle=l),y}}},t.prototype.seriesAfterInit=function(){this.onPointSupported&&this.options.onPoint&&(this.bubblePadding=!0,this.useMapGeometry=!0,this.onPoint=new t(this))},t.prototype.seriesAfterRender=function(){delete this.chart.bubbleZExtremes,this.onPoint&&this.onPoint.drawConnector()},t.prototype.seriesGetCenter=function(t){var o=this.options.onPoint,i=t.positions;if(o){var s=this.chart.get(o.id);s instanceof e&&f(s.plotX)&&f(s.plotY)&&(i[0]=s.plotX,i[1]=s.plotY);var r=o.position;r&&(f(r.x)&&(i[0]=r.x),f(r.y)&&(i[1]=r.y),r.offsetX&&(i[0]+=r.offsetX),r.offsetY&&(i[1]+=r.offsetY))}var n=this.radii&&this.radii[this.index];l(n)&&(i[2]=2*n),t.positions=i},t.prototype.seriesShowOrHide=function(){var t,e=this.chart.series;null===(t=this.points)||void 0===t||t.forEach(function(t){var o=u(e,function(e){var o=((e.onPoint||{}).options||{}).id;return!!o&&o===t.id});o&&o.setVisible(!o.visible,!1)})},t.prototype.seriesTranslate=function(){this.onPoint&&(this.onPoint.getRadii(),this.radii=this.onPoint.radii)},t.prototype.chartGetZData=function(){var t=[];this.series.forEach(function(e){var o=e.options.onPoint;t.push(o&&o.z?o.z:null)}),this.series.forEach(function(e){e.onPoint&&(e.onPoint.zData=e.zData=t)})},t}(),n.Additions=a,p}),o(e,"masters/modules/series-on-point.src.js",[e["Core/Globals.js"],e["Series/SeriesOnPointComposition.js"]],function(t,e){return e.compose(t.Series,t.Chart),t})});