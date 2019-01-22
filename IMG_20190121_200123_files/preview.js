define(["require","exports","tslib","react","external/classnames","modules/clean/search/logger","modules/clean/search/store_helpers"],function(e,t,i,r,s,a,o){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),r=i.__importStar(r),s=i.__importDefault(s),a=i.__importStar(a),o=i.__importStar(o);var n=(function(e){function t(t){var i=e.call(this,t)||this;return i.setSelfDivRef=function(e){i.topDiv=e},i.setImageRef=function(e){i.image=e},i.imageLoadHandler=function(){i.image&&(i.props.visible?i.logCompleted():i.hasStartedLogging&&i.logFailed("user-cancel"),i.isImageTooSmall()||i.setState({imageUrl:i.image.src}))},i.imageErrorHandler=function(){i.setState({imageUrl:void 0}),i.logFailed("server-error")},i.state={imageUrl:void 0},i.previewRequestId=o.generateRandomId(),i}return i.__extends(t,e),t.prototype.previewUrl=function(){return this.props.result.hover_preview_url||void 0},t.prototype.isImageTooSmall=function(){return!this.image||this.image.naturalWidth<=64&&this.image.naturalHeight<=64},t.prototype.basicLogDetails=function(){var e=this.props;return{searchSessionId:e.searchSessionId,result:e.result,user:e.user,previewRequestId:this.previewRequestId}},t.prototype.logStarted=function(){this.hasStartedLogging||(a.logHoverPreviewStarted(this.basicLogDetails()),this.hasStartedLogging=!0)},t.prototype.logCompleted=function(){this.hasFinishedLogging||this.image&&this.topDiv&&(a.logHoverPreviewCompleted(i.__assign({},this.basicLogDetails(),{latency:(new Date).getTime()-this.imageDownloadStart,displayed:!this.isImageTooSmall()&&"none"!==window.getComputedStyle(this.topDiv).display})),this.hasFinishedLogging=!0)},t.prototype.logFailed=function(e){this.hasFinishedLogging||(a.logHoverPreviewFailed(i.__assign({},this.basicLogDetails(),{failureType:e})),this.hasFinishedLogging=!0)},t.prototype.componentDidMount=function(){this.image&&(this.props.visible&&this.logStarted(),this.imageDownloadStart=(new Date).getTime(),this.image.addEventListener("load",this.imageLoadHandler),this.image.addEventListener("error",this.imageErrorHandler))},t.prototype.componentWillReceiveProps=function(e){e.visible&&!this.props.visible&&(this.logStarted(),this.state.imageUrl&&this.logCompleted()),!e.visible&&this.props.visible&&this.logFailed("user-cancel")},t.prototype.render=function(){var e=this.props,t=e.visible,i=e.position;if(!this.previewUrl())return null;var a=void 0!==i&&i<170;return r.createElement("div",{ref:this.setSelfDivRef,className:s.default({"search-bar__preview":!0,"search-bar__preview--visible":t&&!!this.state.imageUrl,"search-bar__preview--align-bottom":a})},r.createElement("div",null,r.createElement("img",{src:this.previewUrl(),ref:this.setImageRef,alt:"","aria-hidden":"true"})))},t})(r.Component);t.SearchBarDropdownPreview=n});
//# sourceMappingURL=preview.min.js-vflyoUQhm.map