#include <iostream>
#include <fstream>
#include <vector>
#include <stdlib.h>
#include <algorithm>

int R, C, F, N, B, T;

class Ride {
public:
	int ord;
	int a, b, x, y, s, f;
	bool assigned = false;

	Ride ()
	{

	}

	Ride (int ord)
	{
		this->ord = ord; 
	}

	Ride& operator= (const Ride& op) 
	{
		if (this != &op) {
			this->ord = op.ord;
			this->a = op.a;
			this->b = op.b;
			this->x = op.x;
			this->y = op.y;
			this->s = op.s;
			this->f = op.f;
			this->assigned = op.assigned;
		}

		return *this;
	}
};

std::vector<Ride> rides;

int getDistance(Ride r)
{
	return abs(r.a - r.x) + abs(r.b - r.y);
}


int getDistance(int a, int b, int x, int y)
{
	return abs(a - x) + abs(b - y);
}

class Car {
public:
	int ord;
	int time;
	bool rideAssigned;
	bool onRide = false;
	bool noRide = false;
	Ride currRide;
	int x, y;
	std::vector<int> ridesDone;

	Car (int ord) {
		this->ord = ord;
		this->x = this->y = 0;
		this->rideAssigned = false;
		this->time = 0;
		this->noRide = this->onRide = false;
		//this->currRide = NULL;
	}

	bool getNextRide()
	{
		int rideOrd = -1;
		bool once = false;

		this->noRide = true;

		for (int i = 0; i < N; ++ i)
			if (rides[i].assigned == false && this->time + getDistance(this->x, this->y, rides[i].a, rides[i].b) + getDistance(rides[i]) < rides[i].f) {
				if (ord != -1 && once == false && time + getDistance(this->x, this->y, rides[i].a, rides[i].b <= rides[i].s)) {
					rideOrd = i;
					once = true;
				}
				else {
					if (rideOrd == -1)
						rideOrd = i;
				}
			}	

		if (rideOrd != -1) {
			this->currRide = rides[rideOrd];
			rides[rideOrd].assigned = true;
			this->noRide = false;
			this->onRide = true;
			return true;
		}

		return false;
	}

	bool step()
	{
		if (this->noRide == true)
			return false;

		if (!this->onRide) {
			this->time += getDistance(this->x, this->y, currRide.a, currRide.b);

			if (this->time < currRide.s)
				this->time = currRide.s;
			
			return true;
		}
		else {
			this->time += getDistance(currRide);
			this->ridesDone.push_back(this->currRide.ord);
			
			return getNextRide();
		}
	}
};

std::vector<Car> fleet;

bool cmp(const Ride& r1, const Ride& r2) {
	if (r1.s == r2.s)
		if (getDistance(r1) == getDistance(r2))
			return r1.f > r2.f;
		else
			return getDistance(r1) > getDistance(r2);

	return r1.s > r2.s;
}

std::istream& operator>>(std::istream& is, Ride& r)
{
	is >> r.a >> r.b >> r.x >> r.y >> r.s >> r.f;
}

std::ostream& operator<<(std::ostream& os, Ride& r)
{
	os << "[" << r.a << ", " << r.b << "]; " << "[" << r.x << ", " << r.y << "]; " << "[" << r.s << ", " << r.f << "]\n";  
}

void _read(char* filename)
{
	std::ifstream fin(filename);

	fin >> R >> C >> F >> N >> B >> T;

	for (int i = 0; i < N; ++ i) {
		Ride tmp;

		fin >> tmp;
		tmp.ord = i;
		rides.push_back(tmp);
	}	

	fin.close();
}

void _print(char* filename)
{
	std::ofstream fout(filename);

	for (size_t i = 0; i < F; ++ i) {
		fout << fleet[i].ridesDone.size() << " ";

		for (const auto& x : fleet[i].ridesDone) {
			fout << x << " ";
		}

		fout << "\n";
	}

	fout.close();
}

void init()
{
	std::sort(rides.begin(), rides.end(), cmp);

	for (int i = 0; i < F; ++ i) {
		fleet.push_back(Car(i));
		fleet[i].getNextRide();
	}
}

void simulation()
{
	bool sw = false;

	// time
	while (!sw) {
		sw = true;

		// car by car
		for (int j = 0; j < F; ++ j) {
			if (fleet[j].step() == true)
				sw = false;
		}
	}
}

int main(int argv, char* argc[])
{
	_read(argc[1]);
	init();
	simulation();
	_print(argc[2]);
	return 0;
}