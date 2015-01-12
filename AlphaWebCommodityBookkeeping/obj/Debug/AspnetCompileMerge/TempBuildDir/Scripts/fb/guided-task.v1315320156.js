function GuidedTask(opts, dispatch) {
	var defaults = {
		position: 2,
		xButton: true,
		buttons: [],
		offset: {
			top:0,
			left:0
		},
		init: function() {}
	};
	this.opts = $.extend(defaults, opts);
	this.dispatch = dispatch;
	
	this.name = this.opts.id;
	this.complete = false;
	
	this.popup = guiders.createGuider(this.opts);
	this.listenForCompleteAction();
}
(function() {
	GuidedTask.prototype.getEvent = function(name) {
		return [this.name, name, 'GuidedTask'].join('.');
	};
	GuidedTask.prototype.showHelp = function() {
		guiders.show(this.opts.id);
	};
	GuidedTask.prototype.markAsDone = function() {
		if (!this.complete) {
			this.complete = true;
			this.dispatch.trigger(this.getEvent('Done'));
		}
	};
	GuidedTask.prototype.listenForCompleteAction = function() {
		this.opts.init(this);
	};
	GuidedTask.prototype.hideHelp = function () {
		this.dispatch.trigger('hideTask', { task: this });
	};
})();

(function() {
	GuidedTask.List = function(task_dfns, dispatcher) {
		var self = this;
		
		this.dispatch = dispatcher || $(document);
		this.tasks = $.map(task_dfns, function(dfn) {
			return new GuidedTask(dfn, self.dispatch);
		});
		$.each(this.tasks, function(i, task) {
			self.dispatch.bind(task.getEvent('Done'), function () {
				self.showNextTask();
			});
		});
		
		this.showingTask = null;
		
		this.dispatch.bind('hideTask', function (e, data) {
			if (data.task === self.showingTask) {
				guiders.hideAll();
			}
		});
	};
	GuidedTask.List.prototype.getTaskByName = function(name) {
		return $.map(this.tasks, function(task) {
			return (task.name == name ? task : null);
		}).shift();
	};
	GuidedTask.List.prototype.getNextIncompleteTask = function() {
		return $.map(this.tasks, function(task) {
			return (task.complete ? null : task);
		}).shift();
	};
	GuidedTask.List.prototype.showNextTask = function() {
		var self = this, 
			nextTask = self.getNextIncompleteTask();
		
		if (self.showingTask === nextTask) {
			return;
		}
		
		guiders.hideAll();
		
		setTimeout(function() {
			if (nextTask) {
				self.showingTask = nextTask;
				nextTask.showHelp();
			}
		}, 1000);
	};
})();

Fresh.ui.GuidedTask = GuidedTask;
