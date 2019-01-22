define(["require","exports","tslib","spectrum_comments/comment_editor/core/class_decorators","spectrum_comments/comment_editor/layers/scaffold","spectrum_comments/comment_editor/core/types","draft-js","spectrum_comments/comment_editor/draft_utils"],function(t,e,o,n,r,s,a,c){"use strict";Object.defineProperty(e,"__esModule",{value:!0});var l=(function(t){function e(){return null!==t&&t.apply(this,arguments)||this}return o.__extends(e,t),e.prototype.initStyle=function(t){var e=t.innerProps,o=e.evt,n=e.value,r=this.splitStyles(o.metadata),s=r.styles;r.other;return a.EditorState.createWithContent(s.reduce(function(t,e){var o=c.locationToSelection(e.location,t);return a.Modifier.applyInlineStyle(t,o,e.style.toUpperCase())},n.getCurrentContent()))},e.prototype.postStyles=function(t){var e=t.innerProps,n=e.evt,r=e.value,s=this.getStyleMetadata(a.convertToRaw(n.getCurrentContent())),c=this.splitStyles(r.metadata).other;return o.__assign({},r,{metadata:c.concat(s)})},e.prototype.getStyleMetadata=function(t){var e=t.blocks,o=e.reduce(function(t,e){var o=t.metadata,n=t.pos,r=e.inlineStyleRanges;return{metadata:o.concat(r.map(function(t){return{type:"style",style:t.style.toLowerCase(),location:{start:n+t.offset,end:n+t.offset+t.length}}})),pos:n+c.decodeUnicode(e.text).length}},{metadata:[],pos:0}),n=o.metadata;o.pos;return n},e.prototype.splitStyles=function(t){return t.reduce(function(t,e){return"style"===e.type?{styles:t.styles.concat([e]),other:t.other}:{styles:t.styles,other:t.other.concat([e])}},{styles:[],other:[]})},o.__decorate([n.plug(r.into.draft.on.init.update.editorState)],e.prototype,"initStyle",null),o.__decorate([n.plug(r.into.comment.on.post.update.content)],e.prototype,"postStyles",null),e})(s.BaseLayer);e.StyleLayer=l});
//# sourceMappingURL=style.min.js-vflGzfhkc.map