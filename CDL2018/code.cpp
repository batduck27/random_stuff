/*
Autor: Mocanu George
https://github.com/batduck27

Solutie pentru problema propusa la CDL 2018, folosind threaduri si alte
functionalitati ale standardului c++17.

Pentru compilare s-a folosit ultima versiune pentru gcc/g++ --> gcc-7, g++-7.
(How to install them: https://askubuntu.com/a/581497)

Dependente: gcc-7, g++-7

btw, problema am mai intalnit-o si aici: http://adventofcode.com/2017/day/18
aici gasiti solutia mea de atunci: https://github.com/batduck27/Advent-of-code-2017/tree/master/day18

btw2, s-ar putea ca la rulare, executabilul sa mai crape - dati vina pe threaduri, nu pe mine :)
*/

#include <iostream>
#include <fstream>
#include <vector>
#include <variant>
#include <iterator>
#include <functional>
#include <queue>
#include <thread>

static const size_t RN = 32;
static const std::string INFILE = "code.in";
static const std::string OUTFILE = "code.out";

typedef std::variant<int, size_t> arg;

class Ins {
private:
	std::string cmd;
	arg op1, op2;

public:
	std::string getCommand() const {
		return cmd;
	}

	arg getST() const {
		return op1;
	}

	arg getND() const {
		return op2;
	}

	friend std::istream& operator>>(std::istream&, Ins&);
	friend std::istream& operator>>(std::istream&, arg&);
	friend std::ostream& operator<<(std::ostream&, Ins&);
	friend std::ostream& operator<<(std::ostream&, arg&);
};

class Program {
private:
	// registers
	std::vector<int> R;
	// instructions
	std::vector<Ins> I;
	// program queue
	std::queue<int> Q;
	// current instruction
	size_t insPt;

	void setRegister(size_t ind, int val) {
		R[ind] = val;
	}

	// returneaza o adresa pentru registru
	int& getRegister(arg op) {
		return R[std::get<size_t>(op)];
	}

public:
	Program(size_t ind, const std::vector<Ins>& ins) : R(RN, 0), I(ins), insPt(0) {
		setRegister(0, (int)ind);
	}

	// returneaza valoarea unui operand
	int getValue(arg op) {
		// valoarea
		if (std::holds_alternative<int>(op))
			return std::get<int>(op);

		// valoare registru
		if (std::holds_alternative<size_t>(op))
			return R[std::get<size_t>(op)];

		return 0;
	}

	void enqueue(int val) {
		Q.push(val);
	}

	int dequeue() {
		int val = Q.front();
		Q.pop();
		return val;
	}

	// se verifica daca programul si-a terminat executia:
	// * daca nu mai sunt instructiuni urmatoare
	// * daca s-a ajuns in situatia de deadlock (instructiunea curenta este "rcv")
	bool deadlock() const {
		return insPt >= I.size() || (insPt < I.size() && I[insPt].getCommand() == "rcv" && Q.empty());
	}

	// se executa instructiunile pana nu mai exista instructiune urmatoare
	void run(Program* prg) {
		while (this->step(prg)) {}
	}

	// executarea unei intructiuni...
	// se returneaza false numai daca s-au terminat intructiunile
	bool step(Program* prg) {
		if (insPt >= I.size())
			return false;
	
		std::string cmd = I[insPt].getCommand();
		arg op1 = I[insPt].getST(), op2 = I[insPt].getND();

		if (cmd == "set") {
			getRegister(op1) = getValue(op2);
			++ insPt;
			return true;
		}

		if (cmd == "add") {
			getRegister(op1) += getValue(op2);
			++ insPt;
			return true;
		}

		if (cmd == "mul") {
			getRegister(op1) *= getValue(op2);
			++ insPt;
			return true;
		}

		if (cmd == "mod") {
			getRegister(op1) %= getValue(op2);
			++ insPt;
			return true;
		}

		if (cmd == "jgz") {
			if (getValue(op1) > 0)
				insPt += getValue(op2);
			else
				++ insPt;

			return true;
		}

		if (cmd == "snd") {
			prg->enqueue(getValue(op1));
			++ insPt;
			return true;
		}

		if (cmd == "rcv") {
			if (Q.empty())
				return true;

			getRegister(op1) = dequeue();
			++ insPt;
			return true;
		}

		++ insPt;

		return false;
	}

	// returneaza valorile finale din registre
	std::vector<int> finalRegisters() {
		std::vector<int> tmp;

		for (const auto& x : R)
			if (x != 0)
				tmp.push_back(x);

		return tmp;
	}
};

std::istream& operator>>(std::istream& is, arg& op) {
	// se ignora spatiul
	if (is.peek() == ' ')
		is.ignore(1);

	// se citeste un registru...
	if (is.peek() == 'R') {
		is.ignore(1);
		size_t ind;
		is >> ind;
		op = ind;
	}
	else if (std::isdigit(is.peek()) || is.peek() == '-') {
		int val; 
		is >> val;
		op = val;
	}

	return is;
}

std::istream& operator>>(std::istream& is, Ins& i) {
	if (is.peek() == '\n')
		is.ignore(1);

	// instructiunea de mai jos ar fi mers daca la finalul fisierelor de
	// input ar fi fost un rand liber pus -> '\n'
	// is >> i.cmd >> i.op1 >> i.op2;

	is >> i.cmd >> i.op1;

	// asa ca trebuie sa fac "magaria" asta :(
	if (i.cmd != "snd" && i.cmd != "rcv")
		is >> i.op2;

	return is;
}

std::ostream& operator<<(std::ostream& os, arg& op) {
	if (std::holds_alternative<size_t>(op))
		os << 'R' << std::get<size_t>(op);
	else if (std::holds_alternative<int>(op))
		os << std::get<int>(op);

	return os;
}

std::ostream& operator<<(std::ostream& os, Ins& i) {
	if (i.cmd == "snd" || i.cmd == "rcv")
		os << i.cmd << " " << i.op1 << "\n";
	else
		os << i.cmd << " " << i.op1 << " " << i.op2 << "\n";

	return os;
}

void _read(std::string filename, std::vector<Ins>& I, size_t& procs) {
	std::ifstream fin(filename);

	fin >> procs;

	I = std::vector<Ins>{std::istream_iterator<Ins>{fin}, {}};

	fin.close();
}

void _print(std::string filename, const std::vector<Program>& P) {
	std::ofstream fout(filename);

	for (auto i : P) {
		for (const auto& j : i.finalRegisters())
			fout << j << " ";

		fout << "\n";
	}

	fout.close();
}

bool deadlock(const std::vector<Program>& P) {
	for (size_t i = 0; i < P.size(); ++ i) {
		if (!P[i].deadlock()) {
			return false;
		}
	}

	return true;
}

int main(int argcv, char* argv[]) {
	std::vector<Ins> I;
	std::vector<Program> P;
	size_t procs;

	_read(INFILE, std::ref(I), std::ref(procs));

	for (size_t i = 0; i < procs; ++ i)
		P.push_back(Program{i, std::ref(I)});

	for (size_t i = 0; i < procs; ++ i)
		std::thread{&Program::run, &P[i], &P[(i + 1) % procs]}.detach();

	while (!deadlock(std::ref(P)));

	_print(OUTFILE, std::ref(P));

	return 0;
}