define(["require","exports","tslib","external/lodash","modules/clean/react/comments2/data/actions","modules/clean/react/comments2/data/types","modules/clean/react/comments2/transforms","modules/clean/uuid"],function(e,n,t,a,d,o,r,i){"use strict";function c(e,n,a,c,s){function l(n){e(d.Actions.activateThreadAndMarkAsRead({thread:n}))}function m(){e(d.Actions.deactivateAllThreads())}return s?t.__assign({},h,{onThreadActivated:l,onAllThreadsDeactivated:m,onMentionsQueryUpdated:c,onThreadCreated:function(n,t){e(d.Actions.createThread({annotation:t,author:r.dbxUserToIUser(s),content:n,guid:i.UUID.v4()})).then(function(){for(var n=0,t=a;n<t.length;n++){var o=t[n];e(d.Actions.markOnboarded(o))}})},onThreadRepliedTo:function(n,t){e(d.Actions.replyToThread({author:r.dbxUserToIUser(s),content:t,threadId:n,guid:i.UUID.v4()}))},onThreadMarkedAsRead:function(n){e(d.Actions.markThreadRead({threadId:n,origin:o.MarkThreadReadOrigin.UIButtonClicked}))},onThreadMarkedAsUnread:function(n){e(d.Actions.markThreadUnread(n))},onThreadResolved:function(n){e(d.Actions.markThreadResolved(n))},onThreadUnresolved:function(n){e(d.Actions.markThreadUnresolved(n))},onCommentEdited:function(n,t,a){e(d.Actions.editComment({commentId:n,content:t,threadId:a}))},onCommentDeleted:function(n,t){e(d.Actions.deleteComment({commentId:n,threadId:t}))}}):t.__assign({},h,{onThreadActivated:l,onAllThreadsDeactivated:m})}Object.defineProperty(n,"__esModule",{value:!0}),a=t.__importStar(a);var s=a.noop,h={onThreadActivated:s,onAllThreadsDeactivated:s,onThreadCreated:s,onThreadRepliedTo:s,onThreadMarkedAsRead:s,onThreadMarkedAsUnread:s,onThreadResolved:s,onThreadUnresolved:s,onCommentEdited:s,onCommentDeleted:s,onMentionsQueryUpdated:s};n.createBasicActionsAdapter=c});
//# sourceMappingURL=basic.min.js-vfl6utm9m.map