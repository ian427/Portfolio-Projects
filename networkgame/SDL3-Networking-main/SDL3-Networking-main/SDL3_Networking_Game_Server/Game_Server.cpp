#include "Server.h"

int main(int argc, char** argv) {
    
    Server server(1234);

    while (server.isRunning()) {
        server.Update();
    }
}