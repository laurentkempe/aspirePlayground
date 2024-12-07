#/bin/bash

sudo wget https://github.com/JanDeDobbeleer/oh-my-posh/releases/latest/download/posh-linux-amd64 -O /usr/local/bin/oh-my-posh
sudo chmod +x /usr/local/bin/oh-my-posh
# echo "$(oh-my-posh init bash)" >> ~/.profile

dapr uninstall --all
dapr init
