define(["require","exports","tslib","react","external/react-dom-factories","modules/clean/file_store/utils","modules/clean/react/file_viewer/coach_mark","modules/clean/react/file_viewer/utils","modules/clean/react_format","modules/clean/static_urls","modules/core/i18n"],function(t,e,o,i,r,n,a,s,l,c,p){"use strict";function d(t,e){if("msft"===e)switch(t){case"word":return h;case"excel":return m;case"powerpoint":return _}if("adbe"===e)switch(t){case"acrobat_or_reader":return f}return null}Object.defineProperty(e,"__esModule",{value:!0}),i=o.__importDefault(i),r=o.__importStar(r);var g=function(t){return l.reactFormat(t,{strong:r.strong()})},u=(function(t){function e(){var e=null!==t&&t.apply(this,arguments)||this;return e.state={},e.setCoachmarkConfig=function(t,o){e.setState({vendor:t,application:o})},e.clearCoachmarkConfig=function(){e.setCoachmarkConfig(void 0,void 0)},e.onReceiveTooltipInfo=function(t){var o=n.getExtension(e.props.file),i=s.TooltipHelpers.getVendorAndApplication(t,o);if(null!==i){var r=i.vendor,a=i.application;e.setCoachmarkConfig(r,a)}},e.hasCoachmark=function(){return e.state.vendor&&e.state.application},e.dismissCoachmark=function(){e.clearCoachmarkConfig()},e.onCoachmarkShown=function(){var t=n.getExtension(e.props.file);s.TooltipHelpers.logImpression(t,e.props.user.id,e.state.application)},e}return o.__extends(e,t),e.prototype.componentDidMount=function(){s.TooltipHelpers.fetchTooltipInfo(this.props.user.id,this.onReceiveTooltipInfo)},e.prototype.render=function(){if(!this.hasCoachmark)return null;var t=this.state,e=t.application,o=t.vendor,r=d(e,o);if(!r)return null;var n=r.dropdownBody,s=r.imageUrl,l=r.imageUrl2x,c=r.imageAlt,g=r.openButtonBody,u=r.title,f={imageUrl:s,imageUrl2x:l,imageAlt:c};return i.default.createElement(a.CoachMark,{arrowPosition:a.ARROW_POSITION.TOP_RIGHT,buttonText:p._("Got it."),imageProps:f,onButtonClick:this.dismissCoachmark,onShown:this.onCoachmarkShown,title:u},this.props.inDropdown?n:g)},e})(i.default.Component);e.OpenButtonCoachmark=u;var f={title:p._("Want to edit this PDF?"),imageUrl:c.static_url("/static/images/file_viewer/coach_mark_pdf-vflIMdX1a.png"),imageUrl2x:c.static_url("/static/images/file_viewer/coach_mark_pdf@2x-vflUkcAgn.png"),imageAlt:p._("An illustration of a PDF file"),openButtonBody:g(p._("You’re looking at a preview. To open this file for editing, click <strong>Open</strong>.")),dropdownBody:p._("You’re looking at a preview. To open this file for editing, click here.")},_={title:p._("Edit shared presentations together"),imageUrl:c.static_url("/static/images/file_viewer/coach_mark_pptx-vfltjppV8.png"),imageUrl2x:c.static_url("/static/images/file_viewer/coach_mark_pptx@2x-vflpvn3tE.png"),imageAlt:p._("An illustration of a Powerpoint file"),openButtonBody:g(p._("Click <strong>open</strong> to edit together in Microsoft PowerPoint Online.")),dropdownBody:g(p._("Choose <strong>Microsoft PowerPoint Online</strong> to edit together."))},m={title:p._("Edit shared spreadsheets together"),imageUrl:c.static_url("/static/images/file_viewer/coach_mark_xlsx-vflaEwaN7.png"),imageUrl2x:c.static_url("/static/images/file_viewer/coach_mark_xlsx@2x-vflkU3Ma1.png"),imageAlt:p._("An illustration of an Excel file"),openButtonBody:g(p._("Click <strong>open</strong> to edit together in Microsoft Excel Online.")),dropdownBody:g(p._("Choose <strong>Microsoft Excel Online</strong> to edit together."))},h={title:p._("Edit shared docs together"),imageUrl:c.static_url("/static/images/file_viewer/coach_mark_docx-vflGsxySJ.png"),imageUrl2x:c.static_url("/static/images/file_viewer/coach_mark_docx@2x-vflJZIQKs.png"),imageAlt:p._("An illustration of a Word file"),openButtonBody:g(p._("Click <strong>open</strong> to edit together in Microsoft Word Online.")),dropdownBody:g(p._("Choose <strong>Microsoft Word Online</strong> to edit together."))}});
//# sourceMappingURL=open_button_coachmark.min.js-vfl9gimeJ.map