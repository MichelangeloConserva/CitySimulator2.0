define(["require","exports","tslib","spectrum_comments/comment_editor/core/class_decorators","spectrum_comments/comment_editor/layers/scaffold","spectrum_comments/comment_editor/core/types","spectrum_comments/comment_editor/components/active_macro_span_component","draft-js","spectrum_comments/comment_editor/draft_utils"],function(t,e,o,r,n,i,a,p,c){"use strict";Object.defineProperty(e,"__esModule",{value:!0});var s=(function(t){function e(e){var o=t.call(this)||this;return o.options=e,o}return o.__extends(e,t),e.prototype.addDecorators=function(t){return t.innerProps.value.concat([{strategy:this.findActiveMacros.bind(this,this.options.type),component:a.ActiveMacroSpanComponent,props:this.options.spanProps}])},e.prototype.checkForActiveMacro=function(t){var e=t.kernel,o=t.triggers,r=this.onMacro,n=this.options,i=n.delimiter,a=n.type,p=t.status.draft.editorState,c=p.getSelection();if(c.isCollapsed()){var s=p.getCurrentContent(),u=c.getAnchorKey(),l=s.getBlockForKey(u),y=c.getAnchorOffset(),d=l.getEntityAt(y)||l.getEntityAt(y-1);if(null!==d&&s.getEntity(d).getType()===a){var f="",g=-1,v=-1;l.findEntityRanges(function(t){return t.getEntity()===d},function(t,e){g=t,v=e,f=l.getText().substring(t,e)});var m=t.status.macro.active;(m&&m.entityKey!==d||f.length&&(!m||m.content!==f))&&o.macro.update(e.compose(r,{delimiter:i,content:f,entityKey:d,blockKey:u,macroStartOffset:g,macroEndOffset:v,type:a})||null)}else o.macro.update(null),o.macro.updateVisibility(!0)}},e.prototype.applyMacroOnRightArrow=function(t){var e=t.status,o=t.innerProps.evt,r=e.macro;if(r.active&&39===o.which&&r.active.type===this.options.type){t.innerProps.value.getSelection().getAnchorOffset()>=r.active.endOffset&&(t.triggers.macro.apply(null),o.preventDefault())}},e.prototype.applyMacroOnReturn=function(t){var e=t.triggers,o=t.status,r=o.macro;r.active&&r.active.type===this.options.type&&e.macro.apply(null)},e.prototype.insertMacro=function(t){function e(t,e,o){var r=t.getSelection();return c.replaceContent(t,c.replaceText(t.getCurrentContent(),r.getFocusKey(),r.getStartOffset(),r.getEndOffset(),e,o))}var o=this.options.delimiter,r=t.status,n=t.innerProps,i=(n.evt,n.value);if(r.macro.active)return i;var a=i.getSelection(),p=i.getCurrentContent().getBlockForKey(a.getAnchorKey()),s=p.getText(),u=a.getStartOffset(),l=0!==u&&" "!==s.substr(u-1,1),y=i;return l&&(y=e(i," ")),e(y,o,y.getCurrentContent().createEntity(this.options.type,"MUTABLE").getLastCreatedEntityKey())},e.prototype.updateActiveMacroEntity=function(t){var e=this.options.delimiter,o=t.innerProps,r=o.evt,n=o.value,i=n.getSelection(),a=n.getCurrentContent(),s=a.getBlockForKey(i.getAnchorKey());if(e.lastIndexOf(r)!==e.length-1){var u=i.getAnchorOffset(),l=s.getEntityAt(u)||s.getEntityAt(u-1);if(null!==l&&a.getEntity(l).getType()===this.options.type){if(" "===r){var y=a;if(s.findEntityRanges(function(t){return t.getEntity()===l},function(t,e){var o=s.getText().substring(t,e);o.match(/\s/)&&(y=c.replaceText(a,i.getAnchorKey(),t,e,o))}),y!==a)return c.replaceContent(n,y)}var d=p.Modifier.replaceText(a,i,r,void 0,l);return c.replaceContent(n,d)}return n}var f=s.getText(),u=i.getAnchorOffset(),g=s.getEntityAt(u)||s.getEntityAt(u-1);if(null!==g&&0===a.getEntity(g).getType().indexOf("macro: ")){var d=p.Modifier.replaceText(a,i,r,void 0,g);return c.replaceContent(n,d)}var v=t.kernel.compose(this.onDelimiter,{text:f,offset:u,input:r,delimiter:e});if(void 0!==v){var m=a.createEntity(this.options.type,"MUTABLE"),h=m.getLastCreatedEntityKey();return c.replaceContent(n,c.replaceText(a,s.getKey(),v.start,v.end,v.text,h))}},e.prototype.updateActiveMacros=function(t){var e=t.innerProps.evt;return null===e?null:e&&e.type===this.options.type?e:void 0},e.prototype.updateMacroRef=function(t){var e=t.status.macro.active,o=t.innerProps.evt;return e&&e.type===this.options.type?o:void 0},e.prototype.updateVisibility=function(t){var e=t.status.macro.active,o=t.innerProps.evt;return e&&e.type!==this.options.type?void 0:o},e.prototype.applyMacro=function(t){var e=t.status.macro;e.active&&e.active.type===this.options.type&&t.kernel.compose(this.onAutoComplete)},e.prototype.findActiveMacros=function(t,e,o,r){e.findEntityRanges(function(e){var o=e.getEntity();return null!==o&&r.getEntity(o).getType()===t},o)},o.__decorate([r.plug(n.into.draft.on.init.update.decorators)],e.prototype,"addDecorators",null),o.__decorate([r.plug(n.into.draft.on.change.update.editorState)],e.prototype,"checkForActiveMacro",null),o.__decorate([r.plug(n.into.draft.on.key.update.editorState)],e.prototype,"applyMacroOnRightArrow",null),o.__decorate([r.plug(n.into.draft.on.returnKey.update.editorState)],e.prototype,"applyMacroOnReturn",null),o.__decorate([r.plug(n.into.draft.on.insertMacro.update.editorState)],e.prototype,"insertMacro",null),o.__decorate([r.plug(n.into.draft.on.input.update.editorState),r.plug(n.into.draft.on.pasteText.update.editorState)],e.prototype,"updateActiveMacroEntity",null),o.__decorate([r.plug(n.into.macro.on.update.update.active)],e.prototype,"updateActiveMacros",null),o.__decorate([r.plug(n.into.macro.on.updateMacroRef.update.macroRef)],e.prototype,"updateMacroRef",null),o.__decorate([r.plug(n.into.macro.on.updateVisibility.update.isVisible)],e.prototype,"updateVisibility",null),o.__decorate([r.plug(n.into.macro.on.apply)],e.prototype,"applyMacro",null),e})(i.BaseLayer);e.MacroLayer=s});
//# sourceMappingURL=macro.min.js-vfl6fFvlj.map