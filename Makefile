.PHONY: init clear

init:
	@echo "🚀 Setting up .NET Develop Environment...\n\n"
	docker-compose up -d

down:
	@echo "😴 Setting down .NET Develop Environment...\n\n"
	docker-compose down