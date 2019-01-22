define(["require","exports","tslib","external/classnames","external/lodash","react","modules/core/i18n","modules/clean/react/size_class/constants"],function(e,t,a,o,n,r,s,u){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),o=a.__importDefault(o),n=a.__importStar(n),r=a.__importDefault(r);var l=(function(e){function t(t){var a=e.call(this,t)||this;return a.textInputRef=function(e){return a.textInput=e},a.debouncedOnPageNumberUpdated=n.debounce(a.props.onPageNumberUpdated,300),a.handlePageIndexChange=function(e){var t=a.props,o=t.currentPageIndex,n=t.pageCount;if(""===e.currentTarget.value||!o||!n)return a.setState({pageNumber:void 0,showSecondaryText:!1}),void a.debouncedOnPageNumberUpdated.cancel();var r=0|+e.target.value;if(isNaN(r)||r<=0||r>n)return void a.setPageNumber(o);a.debouncedOnPageNumberUpdated(r-1),a.setPageNumber(r)},a.onBlur=function(){a.props.currentPageIndex&&a.setPageNumber(a.props.currentPageIndex),a.props.onPageIndicatorBlured()},a.onFocus=function(){a.setState({pageNumber:void 0,showSecondaryText:!1}),a.props.onPageIndicatorFocused()},a.onClick=function(){a.textInput.focus()},a.setPageNumber=function(e){e>=1&&a.props.pageCount&&e<=a.props.pageCount&&a.setState({pageNumber:e,showSecondaryText:!0})},a.state={pageNumber:a.props.currentPageIndex,showSecondaryText:!0},a}return a.__extends(t,e),t.prototype.componentDidUpdate=function(e,t){e.currentPageIndex!==this.props.currentPageIndex&&this.props.currentPageIndex&&this.setPageNumber(this.props.currentPageIndex)},t.prototype.render=function(){var e=this.props,t=e.className,a=e.focused,n=e.pageCount,l=e.sizeClass,c=this.state,i=c.pageNumber,d=c.showSecondaryText,p=s._("Click to jump to a page"),m=d?l===u.SizeClass.Small?"":"  / "+n+" ":"";return r.default.createElement("div",{className:o.default(t,"toolbar-button-entry",{focus:a}),onClick:this.onClick,style:{marginLeft:0,marginRight:0}},r.default.createElement("div",{className:"toolbar-tooltip"},r.default.createElement("div",{className:"toolbar-tooltip-container"},r.default.createElement("div",{className:"toolbar-tooltip-pointer-border"})),r.default.createElement("div",{className:"toolbar-tooltip-container"},r.default.createElement("div",{className:"toolbar-tooltip-body"},p)),r.default.createElement("div",{className:"toolbar-tooltip-container"},r.default.createElement("div",{className:"toolbar-tooltip-pointer"}))),r.default.createElement("div",{className:"content"},r.default.createElement("input",{className:o.default("page-input",{focus:a,"with-text":!!i&&a}),alt:p,placeholder:s._("Page..."),onChange:this.handlePageIndexChange,onBlur:this.onBlur,onFocus:this.onFocus,value:i?i:"",ref:this.textInputRef}),m))},t})(r.default.PureComponent);t.PageIndicator=l});
//# sourceMappingURL=preview_toolbar_page_indicator.min.js-vflAYPUml.map