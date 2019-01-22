define(["require","exports","tslib","spectrum_comments/comment_editor/core/class_decorators","spectrum_comments/comment_editor/layers/scaffold","spectrum_comments/comment_editor/core/types"],function(t,o,n,e,r,c){"use strict";function l(t){return{isEmpty:!t.getCurrentContent().getPlainText().length,isFocused:t.getSelection().getHasFocus()}}Object.defineProperty(o,"__esModule",{value:!0});var s=(function(t){function o(o){var n=t.call(this)||this;return n.options=o,n}return n.__extends(o,t),o.prototype.callOnContentChange=function(t){var o=t.status.draft.editorState,n=t.innerProps.value;o!==n&&this.options.onContentChange(l(n))},o.prototype.callOnPost=function(t){var o=t.innerProps.value.content;this.options.onPost(o)},o.prototype.callOnCancel=function(){this.options.onCancel()},o.prototype.callOnFocus=function(t){this.options.onFocus(t.innerProps.evt)},o.prototype.callOnBlur=function(t){this.options.onBlur(t.innerProps.evt)},n.__decorate([e.plug(r.into.draft.on.change.update.editorState)],o.prototype,"callOnContentChange",null),n.__decorate([e.plug(r.into.comment.on.post)],o.prototype,"callOnPost",null),n.__decorate([e.plug(r.into.comment.on.cancel)],o.prototype,"callOnCancel",null),n.__decorate([e.plug(r.into.draft.on.focus)],o.prototype,"callOnFocus",null),n.__decorate([e.plug(r.into.draft.on.blur)],o.prototype,"callOnBlur",null),o})(c.BaseLayer);o.PublishLayer=s});
//# sourceMappingURL=publish.min.js-vflhhpmVQ.map