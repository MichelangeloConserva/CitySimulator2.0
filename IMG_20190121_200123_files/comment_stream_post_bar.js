define(["require","exports","tslib","react","spectrum/button","spectrum_comments/comment_stream/mention_icon","spectrum_comments/comment_editor/components/post_bar","prop-types"],function(e,t,n,o,c,l,r,a){"use strict";function i(e){return o.createElement(c.Button,{"aria-label":e.label,className:"sc-comment-editor-control-btn",onClick:function(){return e.onClick()}},o.createElement(l.MentionIcon,null))}function s(e){return o.createElement("div",{className:"sc-comment-editor-inline-controls"},e.children)}function m(e,t){var c=t.localization,l=e.signals,a=e.postEnabled,m=e.cancelEnabled,u=e.isEmpty,b=o.createElement(i,{label:c.mentionButtonLabel,onClick:l.insertMention});return!u||a||m?o.createElement(r.PostBar,n.__assign({},e,{postLabel:c.streamPostLabel,cancelLabel:c.streamCancelLabel}),b):o.createElement(s,null,b)}Object.defineProperty(t,"__esModule",{value:!0}),t.MentionButton=i,t.InlineControls=s,t.CommentStreamPostBar=m,m.contextTypes={localization:a.object}});
//# sourceMappingURL=comment_stream_post_bar.min.js-vflU-FG0z.map