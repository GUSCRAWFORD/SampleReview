describe('reviewResource', function () {
    var reviewResource, $httpBackend;
    beforeEach(function () { module('app.review') });
    beforeEach(inject(function (_$httpBackend_, _reviewResource_) {
        reviewResource = _reviewResource_;
        $httpBackend = _$httpBackend_;
    }));
    describe('.query', function () {
        it('is a function', function () { expect(typeof (reviewResource.query)).toBe('function') });
        it('calls api a particular way', function () {

        });
    });
    describe('.get', function () {
        it('is a function', function () { expect(typeof (reviewResource.get)).toBe('function') });
        it('calls api a particular way', function () { });
    });
    describe('.save', function () {
        it('is a function', function () { expect(typeof (reviewResource.save)).toBe('function') });
        it('calls api a particular way', function () { });
    });
});

/*var angular = require('angular');
angular.module('app.smithReview').service('reviewResource', reviewResource);
reviewResource.$inject = ['$resource', 'restEndpoint', 'smithConstraints'];
function reviewResource($resource, restEndpoint, smithConstraints) {
	return $resource(restEndpoint + 'reviews', {
		item:'@item'
	});
}*/