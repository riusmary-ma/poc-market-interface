# poc-market-interface
A poc of market-interface when Allegement come in from market and try to match with Instruction in Security Core system. 
They will try Exact match when every mandatory fields need to be matched exactly. 
When exact match failed, will try to close match these 2 message. 
If lots of mandatory fields are all matching failed, then get the no match result.

---------------------
How to run this app on your desktop?
1. download repository.
2. run "dotnet run --project PocMarketInterface.csproj --urls http://localhost:5000" to start backend.
3. run "npm start" to start frontend. *If you didn't install node.js please install it at first, then try npm install.
4. or try Docker running, run "docker build -t poc-market-interface:latest ." and input port as 3000.
5. visit this url "http://localhost:3000/".