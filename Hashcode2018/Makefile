build:
	g++ -std=c++11 gtfo.cpp -o  exe

run: clean build
	./exe a_example.in a_example.out
	./exe b_should_be_easy.in b_should_be_easy.out
	./exe c_no_hurry.in c_no_hurry.out
	./exe d_metropolis.in d_metropolis.out
	./exe e_high_bonus.in e_high_bonus.out
clean:
	rm -f *.out exe
